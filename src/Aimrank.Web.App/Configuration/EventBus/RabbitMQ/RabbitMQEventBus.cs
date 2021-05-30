using Aimrank.Web.Common.Application.Events;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Aimrank.Web.App.Configuration.EventBus.RabbitMQ
{
    public class RabbitMQEventBus : IEventBus
    {
        private readonly IEventBus _eventBus;
        private readonly RabbitMQSettings _rabbitMqSettings;
        private readonly RabbitMQEventSerializer _eventSerializer;
        private readonly RabbitMQRoutingKeyFactory _routingKeyFactory;

        public RabbitMQEventBus(
            IEventBus eventBus,
            IOptions<RabbitMQSettings> rabbitMqSettings,
            RabbitMQEventSerializer eventSerializer,
            RabbitMQRoutingKeyFactory routingKeyFactory)
        {
            _eventBus = eventBus;
            _rabbitMqSettings = rabbitMqSettings.Value;
            _eventSerializer = eventSerializer;
            _routingKeyFactory = routingKeyFactory;
        }

        public async Task Publish(IIntegrationEvent @event)
        {
            await _eventBus.Publish(@event);

            if (IsOutboundEvent(@event))
            {
                PublishEventsToRabbitMQ(new[] {@event});
            }
        }

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