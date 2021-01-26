using Aimrank.Application;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Aimrank.Web.Events.Handlers
{
    public class IntegrationEventGenericHandler<T> : IIntegrationEventHandler<T>
        where T : IntegrationEvent
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public IntegrationEventGenericHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task HandleAsync(T @event)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var eventHandlerOpenType = typeof(IIntegrationEventHandler<>);
            var eventHandlerClosedType = eventHandlerOpenType.MakeGenericType(@event.GetType());

            var handlers = scope.ServiceProvider.GetServices(eventHandlerClosedType);

            foreach (dynamic handler in handlers)
            {
                await handler.HandleAsync(@event);
            }
        }
    }
}