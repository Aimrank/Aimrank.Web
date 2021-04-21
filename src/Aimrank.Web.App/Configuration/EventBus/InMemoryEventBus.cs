using Aimrank.Web.Common.Application.Events;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.App.Configuration.EventBus
{
    internal sealed class InMemoryEventBus
    {
        public static InMemoryEventBus Instance { get; } = new();

        public IEnumerable<Type> Events => _handlers.Keys;

        private readonly Dictionary<Type, List<IIntegrationEventHandler>> _handlers = new();

        private InMemoryEventBus()
        {
        }
        
        public async Task Publish<TEvent>(TEvent @event) where TEvent : IIntegrationEvent
        {
            var integrationEventHandlers = _handlers.GetValueOrDefault(@event.GetType());
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
            var type = typeof(TEvent);

            if (_handlers.ContainsKey(type))
            {
                _handlers[type].Add(handler);
            }
            else
            {
                _handlers[type] = new List<IIntegrationEventHandler> {handler};
            }
        }
    }
}