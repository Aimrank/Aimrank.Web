using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Common.Infrastructure.EventBus.RabbitMQ;
using Autofac;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Collections.Generic;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Rabbit
{
    internal class RabbitMQEventBus : RabbitMQEventBusBase
    {
        public RabbitMQEventBus(RabbitMQSettings rabbitMqSettings, ILogger logger) : base(rabbitMqSettings, logger)
        {
        }

        public override void Listen()
        {
            if (Events.Count == 0)
            {
                return;
            }

            var consumer = new AsyncEventingBasicConsumer(Channel);

            consumer.Received += async (_, ea) =>
            {
                var integrationEvent = DeserializeEvent(ea.RoutingKey, ea.Body.ToArray());
                var integrationEventType = integrationEvent.GetType();
                var integrationEventHandlerType = typeof(IIntegrationEventHandler<>).MakeGenericType(integrationEventType);
                var integrationEventHandlersCollectionType = typeof(IEnumerable<>).MakeGenericType(integrationEventHandlerType);

                await using var scope = MatchesCompositionRoot.BeginLifetimeScope();

                dynamic handlers = scope.Resolve(integrationEventHandlersCollectionType);

                foreach (var handler in handlers)
                {
                    await handler.HandleAsync((dynamic) integrationEvent);
                }
                
                Channel.BasicAck(ea.DeliveryTag, false);
            };

            Channel.BasicConsume(RabbitMQSettings.ServiceName, false, consumer: consumer);
        }
    }
}