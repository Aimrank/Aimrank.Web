using Aimrank.Web.Common.Application.Events;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.App.Configuration.EventBus
{
    internal class InMemoryEventBus : IEventBus
    {
        private readonly IServiceProvider _serviceProvider;

        public InMemoryEventBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Publish(IIntegrationEvent @event)
        {
            using var scope = _serviceProvider.CreateScope();
            var handlerType = typeof(IIntegrationEventHandler<>).MakeGenericType(@event.GetType());
            var handlers = scope.ServiceProvider.GetServices(handlerType);
            
            foreach (var handler in handlers)
            {
                if (handler is null)
                {
                    return;
                }
                
                var method = handlerType.GetMethod(nameof(IIntegrationEventHandler<IIntegrationEvent>.HandleAsync));
                await (Task) method.Invoke(handler, new object[]{@event, CancellationToken.None});
            }
        }
    }
}