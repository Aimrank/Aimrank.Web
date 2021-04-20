using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Common.Infrastructure.EventBus;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Exceptions;
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
        private readonly ILogger<RabbitMQEventBus> _logger;
        private readonly RabbitMQSettings _rabbitMqSettings;
        private readonly RabbitMQEventSerializer _eventSerializer;
        private readonly RabbitMQRoutingKeyFactory _routingKeyFactory;

        public RabbitMQEventBus(
            IEventBus eventBus,
            ILogger<RabbitMQEventBus> logger,
            RabbitMQSettings rabbitMqSettings,
            RabbitMQEventSerializer eventSerializer,
            RabbitMQRoutingKeyFactory routingKeyFactory)
        {
            _eventBus = eventBus;
            _logger = logger;
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
                await PublishEventsToRabbitMQ(new[] {@event});
            }
        }

        public async Task Publish(IEnumerable<IIntegrationEvent> events)
        {
            var list = events.ToList();
            
            foreach (var @event in list)
            {
                await _eventBus.Publish(@event);
            }
            
            await PublishEventsToRabbitMQ(list.Where(IsOutboundEvent));
        }

        public IEventBus Subscribe<TEvent>(IIntegrationEventHandler<TEvent> handler) where TEvent : IIntegrationEvent
            => _eventBus.Subscribe(handler);

        private async Task PublishEventsToRabbitMQ(IEnumerable<IIntegrationEvent> events)
        {
            var channel = await CreateModel();
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
            return attribute?.Outbound ?? false;
        }
        
        private async Task<IModel> CreateModel()
        {
            var factory = new ConnectionFactory
            {
                HostName = _rabbitMqSettings.HostName,
                Port = _rabbitMqSettings.Port,
                UserName = _rabbitMqSettings.Username,
                Password = _rabbitMqSettings.Password,
                DispatchConsumersAsync = true
            };

            while (true)
            {
                try
                {
                    var connection = factory.CreateConnection();
                    var channel = connection.CreateModel();
                    channel.ExchangeDeclare(_rabbitMqSettings.ExchangeName, "direct", true, false, null);
                    return channel;
                }
                catch (BrokerUnreachableException)
                {
                    _logger.LogError("Failed to connect to RabbitMQ. Retrying in 10 seconds.");

                    await Task.Delay(10000);
                }
            }
        }
    }
}