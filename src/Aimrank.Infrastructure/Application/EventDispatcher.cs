using Aimrank.Application;
using Aimrank.Common.Application;
using Aimrank.Common.Infrastructure.EventBus;
using System.Threading.Tasks;

namespace Aimrank.Infrastructure.Application
{
    internal class EventDispatcher : IEventDispatcher
    {
        private readonly IEventBus _eventBus;

        public EventDispatcher(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task DispatchAsync(IntegrationEvent @event) => _eventBus.Publish(@event);
    }
}