using Aimrank.Web.Common.Application.Events;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.App.Configuration.EventBus.RabbitMQ
{
    public class RabbitMQBackgroundService : BackgroundService
    {
        private readonly RabbitMQSettings _rabbitMqSettings;
        private readonly RabbitMQEventSerializer _eventSerializer;
        private readonly RabbitMQEventRegistry _eventRegistry;
        private readonly ILogger<RabbitMQBackgroundService> _logger;
        private readonly IEventBus _eventBus;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQBackgroundService(
            IOptions<RabbitMQSettings> rabbitMqSettings,
            RabbitMQEventSerializer eventSerializer,
            RabbitMQEventRegistry eventRegistry,
            ILogger<RabbitMQBackgroundService> logger,
            IEventBus eventBus)
        {
            _rabbitMqSettings = rabbitMqSettings.Value;
            _eventSerializer = eventSerializer;
            _eventRegistry = eventRegistry;
            _logger = logger;
            _eventBus = eventBus;
            
            _connection = CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(_rabbitMqSettings.ExchangeName, "direct", true, false, null);
            _channel.QueueDeclare(_rabbitMqSettings.ServiceName, true, false, false, null);

            foreach (var routingKey in _eventRegistry.GetRoutingKeys())
            {
                _channel.QueueBind(_rabbitMqSettings.ServiceName, _rabbitMqSettings.ExchangeName, routingKey);
            }
        }

        public override void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
            base.Dispose();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            
            consumer.Received += async (_, ea) =>
            {
                var types = _eventRegistry.GetEventsForRoutingKey(ea.RoutingKey).ToList();
                if (types.Count == 0)
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

            return Task.CompletedTask;
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
    }
}