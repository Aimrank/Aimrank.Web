using Aimrank.Web.Common.Application.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.App.Configuration.EventBus.RabbitMQ
{
    internal class RabbitMQBackgroundService : BackgroundService
    {
        private readonly RabbitMQSettings _rabbitMqSettings;
        private readonly RabbitMQEventSerializer _eventSerializer;
        private readonly RabbitMQRoutingKeyFactory _routingKeyFactory;
        private readonly ILogger<RabbitMQBackgroundService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IEventBus _eventBus;
        private IBasicProperties _basicProperties;
        private IConnection _connection;
        private IModel _channel;

        private Dictionary<string, List<Type>> Events { get; } = new();

        public RabbitMQBackgroundService(
            IOptions<RabbitMQSettings> rabbitMqSettings,
            RabbitMQEventSerializer eventSerializer,
            RabbitMQRoutingKeyFactory routingKeyFactory,
            ILogger<RabbitMQBackgroundService> logger,
            IServiceProvider serviceProvider,
            IEventBus eventBus)
        {
            _rabbitMqSettings = rabbitMqSettings.Value;
            _eventSerializer = eventSerializer;
            _routingKeyFactory = routingKeyFactory;
            _logger = logger;
            _serviceProvider = serviceProvider;
            _eventBus = eventBus;
        }

        public override void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
            base.Dispose();
        }
        
        public void Configure()
        {
            _connection = CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(_rabbitMqSettings.ExchangeName, "direct", true, false, null);
            _channel.QueueDeclare(_rabbitMqSettings.ServiceName, true, false, false, null);
            _basicProperties = _channel.CreateBasicProperties();
            _basicProperties.Persistent = true;

            foreach (var (type, attribute) in GetSubscribedEventTypes())
            {
                var routingKey = _routingKeyFactory.Create(type, attribute.Service);
                if (Events.ContainsKey(routingKey))
                {
                    Events[routingKey].Add(type);
                }
                else
                {
                    Events[routingKey] = new List<Type> {type};
                }
            }

            foreach (var routingKey in Events.Keys)
            {
                _channel.QueueBind(_rabbitMqSettings.ServiceName, _rabbitMqSettings.ExchangeName, routingKey);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (_channel is null)
            {
                await Task.Delay(1000, stoppingToken);
            }
            
            var consumer = new AsyncEventingBasicConsumer(_channel);
            
            consumer.Received += async (_, ea) =>
            {
                var types = Events.GetValueOrDefault(ea.RoutingKey);
                if (types is null || types.Count == 0)
                {
                    _logger.LogWarning($"No events were mapped for routing key '{ea.RoutingKey}'.");
                    return;
                }
                
                var events = _eventSerializer.Deserialize(ea.Body.ToArray(), types);
                
                foreach (var @event in events)
                {
                    await _eventBus.Publish(@event);
                }
                
                _channel.BasicAck(ea.DeliveryTag, false);
            };
            
            _channel.BasicConsume(_rabbitMqSettings.ServiceName, false, consumer: consumer);
        }

        private IConnection CreateConnection()
        {
            var factory = new ConnectionFactory
            {
                HostName = _rabbitMqSettings.HostName,
                Port = _rabbitMqSettings.Port,
                UserName = _rabbitMqSettings.Username,
                Password = _rabbitMqSettings.Password,
                DispatchConsumersAsync = true
            };
            
            return factory.CreateConnection();
        }

        private IEnumerable<EventType> GetSubscribedEventTypes()
        {
            using var scope = _serviceProvider.CreateScope();
            return scope.ServiceProvider.GetServices(typeof(IIntegrationEventHandler))
                .Select(handler => handler?.GetType().GetGenericArguments().FirstOrDefault())
                .Where(type => type is not null)
                .Where(type =>
                {
                    var attribute = (IntegrationEventAttribute) type.GetCustomAttribute(typeof(IntegrationEventAttribute));
                    return attribute is not null && attribute.Type == IntegrationEventType.Inbound;
                })
                .Select(type => new EventType(type, (IntegrationEventAttribute) type.GetCustomAttribute(typeof(IntegrationEventAttribute))))
                .ToList();
        }

        private record EventType(Type Type, IntegrationEventAttribute Attribute);
    }
}