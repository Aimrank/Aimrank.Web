using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Common.Infrastructure.EventBus;
using System.Threading.Tasks;

namespace Aimrank.Web.Modules.CSGO.Infrastructure.Configuration.Processing
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