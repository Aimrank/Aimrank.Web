using Aimrank.Common.Application.Events;
using Aimrank.Common.Infrastructure.EventBus;
using System.Threading.Tasks;

namespace Aimrank.Modules.CSGO.Infrastructure.Configuration.Processing
{
    internal class EventDispatcher : IEventDispatcher
    {
        private readonly IEventBus _eventBus;

        public EventDispatcher(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task DispatchAsync(IIntegrationEvent @event) => _eventBus.Publish(@event);

        public Task DispatchAsync() => Task.CompletedTask;
    }
}