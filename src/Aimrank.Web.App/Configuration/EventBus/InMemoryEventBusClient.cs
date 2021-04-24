using Aimrank.Web.Common.Application.Events;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.App.Configuration.EventBus
{
    internal class InMemoryEventBusClient : IEventBus
    {
        public IEnumerable<Type> Events => InMemoryEventBus.Instance.Events;

        public async Task Publish(IIntegrationEvent @event)
        {
            await InMemoryEventBus.Instance.Publish(@event);
        }

        public async Task Publish(IEnumerable<IIntegrationEvent> events)
        {
            foreach (var @event in events)
            {
                await Publish(@event);
            }
        }

        public IEventBus Subscribe<TEvent>(IIntegrationEventHandler<TEvent> handler) where TEvent : IIntegrationEvent
        {
            InMemoryEventBus.Instance.Subscribe(handler);
            return this;
        }
    }
}