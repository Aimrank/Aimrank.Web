using Aimrank.Common.Application.Events;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text;
using System;

namespace Aimrank.Common.Infrastructure.EventBus.RabbitMQ
{
    public abstract class RabbitMQEventBusBase : IRabbitMQEventBus, IDisposable
    {
        protected RabbitMQSettings RabbitMQSettings { get; }
        protected IBasicProperties BasicProperties { get; }
        protected IConnection Connection { get; }
        protected IModel Channel { get; }
        
        protected Dictionary<string, Type> Events { get; } = new();

        protected RabbitMQEventBusBase(RabbitMQSettings rabbitMqSettings)
        {
            RabbitMQSettings = rabbitMqSettings;

            var factory = new ConnectionFactory
            {
                HostName = RabbitMQSettings.HostName,
                Port = RabbitMQSettings.Port,
                UserName = RabbitMQSettings.Username,
                Password = RabbitMQSettings.Password,
                DispatchConsumersAsync = true
            };

            Connection = factory.CreateConnection();
            Channel = Connection.CreateModel();
            Channel.ExchangeDeclare(RabbitMQSettings.ExchangeName, "direct", true, false, null);
            Channel.QueueDeclare(RabbitMQSettings.ServiceName, true, false, false, null);
            
            BasicProperties = Channel.CreateBasicProperties();
            BasicProperties.Persistent = true;
        }
        
        public void Dispose()
        {
            Channel?.Close();
            Connection?.Dispose();
        }
        
        public virtual void Publish(IIntegrationEvent @event)
            => Channel.BasicPublish(RabbitMQSettings.ExchangeName, GetRoutingKey(@event.GetType()),
                BasicProperties, SerializeEvent(@event));

        public virtual void Subscribe<T>() where T : IIntegrationEvent
        {
            var attribute = (IntegrationEventAttribute) typeof(T).GetCustomAttribute(typeof(IntegrationEventAttribute));
            if (attribute is null)
            {
                throw new Exception($"Missing IntegrationEventAttribute on class {typeof(T).Name}");
            }

            var routingKey = GetRoutingKey(typeof(T), attribute.Service);
            Events[routingKey] = typeof(T);
            Channel.QueueBind(RabbitMQSettings.ServiceName, RabbitMQSettings.ExchangeName, routingKey);
        }
        
        public abstract void Listen();

        private string GetRoutingKey(Type type, string serviceName = null)
            => serviceName is null
                ? $"{RabbitMQSettings.ServiceName}.{type.Name}"
                : $"{serviceName}.{type.Name}";

        private byte[] SerializeEvent(IIntegrationEvent @event)
        {
            var text = JsonSerializer.Serialize(@event, @event.GetType());
            return Encoding.UTF8.GetBytes(text);
        }

        protected IIntegrationEvent DeserializeEvent(string routingKey, byte[] data)
        {
            var text = Encoding.UTF8.GetString(data);
            var type = Events.GetValueOrDefault(routingKey);
            if (type is null)
            {
                throw new Exception("Unsupported event type.");
            }
            
            return (IIntegrationEvent) JsonSerializer.Deserialize(text, type);
        }
    }
}