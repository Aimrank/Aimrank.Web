using Aimrank.Common.Application.Events;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Aimrank.Modules.CSGO.Infrastructure.Configuration.Rabbit
{
    internal class RabbitMQEventBus : IIntegrationEventBus, IDisposable
    {
        private readonly RabbitMQSettings _rabbitSettings;
        private readonly IBasicProperties _basicProperties;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        
        private readonly Dictionary<string, Type> _events = new();

        public RabbitMQEventBus(RabbitMQSettings rabbitSettings)
        {
            _rabbitSettings = rabbitSettings;

            var factory = new ConnectionFactory
            {
                HostName = _rabbitSettings.HostName,
                Port = _rabbitSettings.Port,
                UserName = _rabbitSettings.Username,
                Password = _rabbitSettings.Password,
                DispatchConsumersAsync = true
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(_rabbitSettings.ExchangeName, "direct", true, false, null);
            _channel.QueueDeclare(_rabbitSettings.ServiceName, true, false, false, null);
            
            _basicProperties = _channel.CreateBasicProperties();
            _basicProperties.Persistent = true;
        }
        
        public void Dispose()
        {
            _channel?.Close();
            _connection?.Dispose();
        }
        
        public void Publish(IIntegrationEvent @event)
            => _channel.BasicPublish(_rabbitSettings.ExchangeName, GetRoutingKey(@event.GetType()),
                _basicProperties, SerializeEvent(@event));

        public IIntegrationEventBus Subscribe<T>(string serviceName) where T : IIntegrationEvent
        {
            var routingKey = GetRoutingKey(typeof(T), serviceName);
            _events[routingKey] = typeof(T);
            _channel.QueueBind(_rabbitSettings.ServiceName, _rabbitSettings.ExchangeName, routingKey);
            return this;
        }
        
        public void Listen()
        {
            if (_events.Count == 0)
            {
                return;
            }

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.Received += (_, ea) =>
            {
                var @event = DeserializeEvent(ea.RoutingKey, ea.Body.ToArray());
                
                Console.WriteLine($"Received event: {@event.GetType().Name}");
                
                _channel.BasicAck(ea.DeliveryTag, false);

                return Task.CompletedTask;
            };
            
            _channel.BasicConsume(_rabbitSettings.ServiceName, false, consumer: consumer);
        }

        private string GetRoutingKey(Type type, string serviceName = null)
            => serviceName is null
                ? $"{_rabbitSettings.ServiceName}.{type.Name}"
                : $"{serviceName}.{_events.GetType().Name}";

        private byte[] SerializeEvent(IIntegrationEvent @event)
        {
            var text = JsonSerializer.Serialize(@event, @event.GetType());
            return Encoding.UTF8.GetBytes(text);
        }

        private IIntegrationEvent DeserializeEvent(string routingKey, byte[] data)
        {
            var text = Encoding.UTF8.GetString(data);
            var type = _events.GetValueOrDefault(routingKey);
            if (type is null)
            {
                throw new Exception("Unsupported event type.");
            }
            
            return (IIntegrationEvent) JsonSerializer.Deserialize(text, type);
        }
    }
}