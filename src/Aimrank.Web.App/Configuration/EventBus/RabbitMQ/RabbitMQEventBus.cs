using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Common.Infrastructure.EventBus;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.App.Configuration.EventBus.RabbitMQ
{
    internal class RabbitMQEventBus : IEventBus
    {
        private readonly IEventBus _eventBus;
        private readonly RabbitMQSettings _rabbitMqSettings;
        private readonly RabbitMQEventSerializer _eventSerializer;
        private readonly RabbitMQRoutingKeyFactory _routingKeyFactory;

        public RabbitMQEventBus(
            IEventBus eventBus,
            RabbitMQSettings rabbitMqSettings,
            RabbitMQEventSerializer eventSerializer,
            RabbitMQRoutingKeyFactory routingKeyFactory)
        {
            _eventBus = eventBus;
            _rabbitMqSettings = rabbitMqSettings;
            _eventSerializer = eventSerializer;
            _routingKeyFactory = routingKeyFactory;
        }

        public IEnumerable<Type> Events => _eventBus.Events;
        
        public async Task Publish(IIntegrationEvent @event)
        {
            await _eventBus.Publish(@event);

            if (IsOutboundEvent(@event))
            {
                PublishEventsToRabbitMQ(new[] {@event});
            }
        }

        public async Task Publish(IEnumerable<IIntegrationEvent> events)
        {
            var list = events.ToList();
            
            foreach (var @event in list)
            {
                await _eventBus.Publish(@event);
            }
            
            PublishEventsToRabbitMQ(list.Where(IsOutboundEvent));
        }

        public IEventBus Subscribe<TEvent>(IIntegrationEventHandler<TEvent> handler) where TEvent : IIntegrationEvent
            => _eventBus.Subscribe(handler);

        private void PublishEventsToRabbitMQ(IEnumerable<IIntegrationEvent> events)
        {
            var channel = CreateChannel();
            var basicProperties = channel.CreateBasicProperties();
            basicProperties.Persistent = true;
            
            foreach (var @event in events)
            {
                channel.BasicPublish(
                    _rabbitMqSettings.ExchangeName,
                    _routingKeyFactory.Create(@event.GetType()),
                    basicProperties, 
                    _eventSerializer.Serialize(@event));
            }
        }

        private static bool IsOutboundEvent(IIntegrationEvent @event)
        {
            var attribute = (IntegrationEventAttribute) @event.GetType().GetCustomAttribute(typeof(IntegrationEventAttribute));
            return attribute?.Type == IntegrationEventType.Outbound;
        }
        
        private IModel CreateChannel()
        {
            var factory = new ConnectionFactory
            {
                HostName = _rabbitMqSettings.HostName,
                Port = _rabbitMqSettings.Port,
                UserName = _rabbitMqSettings.Username,
                Password = _rabbitMqSettings.Password,
                DispatchConsumersAsync = true
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.ExchangeDeclare(_rabbitMqSettings.ExchangeName, "direct", true, false, null);
            return channel;
        }
    }
}