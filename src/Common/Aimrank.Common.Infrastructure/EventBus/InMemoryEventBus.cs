using Aimrank.Common.Application.Events;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aimrank.Common.Infrastructure.EventBus
{
    internal sealed class InMemoryEventBus
    {
        public static InMemoryEventBus Instance { get; } = new();

        private readonly Dictionary<string, List<IIntegrationEventHandler>> _handlers = new();

        private InMemoryEventBus()
        {
        }
        
        public async Task Publish<TEvent>(TEvent @event) where TEvent : IIntegrationEvent
        {
            var eventName = @event.GetType().FullName;

            var integrationEventHandlers = _handlers.GetValueOrDefault(eventName);
            if (integrationEventHandlers is not null)
            {
                foreach (dynamic integrationEventHandler in integrationEventHandlers)
                {
                    await integrationEventHandler.HandleAsync((dynamic) @event);
                }
            }
        }

        public void Subscribe<TEvent>(IIntegrationEventHandler<TEvent> handler) where TEvent : IIntegrationEvent
        {
            var eventName = typeof(TEvent).FullName;

            if (_handlers.ContainsKey(eventName))
            {
                _handlers[eventName].Add(handler);
            }
            else
            {
                _handlers[eventName] = new List<IIntegrationEventHandler> {handler};
            }
        }
    }
}