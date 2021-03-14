using Aimrank.Common.Application.Events;
using Autofac;
using System.Collections.Generic;
using System.Collections;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Configuration.EventBus
{
    public class IntegrationEventGenericHandler<T> : IIntegrationEventHandler<T>
        where T : IIntegrationEvent
    {
        private readonly ILifetimeScope _lifetimeScope;

        public IntegrationEventGenericHandler(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public async Task HandleAsync(T @event, CancellationToken cancellationToken = default)
        {
            await using var scope = _lifetimeScope.BeginLifetimeScope();

            var eventHandlerOpenType = typeof(IIntegrationEventHandler<>);
            var eventHandlerClosedType = eventHandlerOpenType.MakeGenericType(@event.GetType());
            var eventHandlersType = typeof(IEnumerable<>).MakeGenericType(eventHandlerClosedType);

            var handlers = (IEnumerable) scope.Resolve(eventHandlersType);
            
            foreach (dynamic handler in handlers)
            {
                await handler.HandleAsync(@event);
            }
        }
    }
}