using Aimrank.Web.Common.Application.Events;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Collections;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.App.Configuration.EventBus
{
    public class IntegrationEventGenericHandler<T> : IIntegrationEventHandler<T>
        where T : IIntegrationEvent
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public IntegrationEventGenericHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task HandleAsync(T @event, CancellationToken cancellationToken = default)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var eventHandlerOpenType = typeof(IIntegrationEventHandler<>);
            var eventHandlerClosedType = eventHandlerOpenType.MakeGenericType(@event.GetType());
            var eventHandlersType = typeof(IEnumerable<>).MakeGenericType(eventHandlerClosedType);

            var handlers = (IEnumerable) scope.ServiceProvider.GetRequiredService(eventHandlersType);
            
            foreach (dynamic handler in handlers)
            {
                await handler.HandleAsync((dynamic) @event);
            }
        }
    }
}