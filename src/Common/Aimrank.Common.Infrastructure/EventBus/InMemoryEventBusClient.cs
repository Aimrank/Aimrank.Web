using Aimrank.Common.Application.Events;
using System.Threading.Tasks;

namespace Aimrank.Common.Infrastructure.EventBus
{
    public class InMemoryEventBusClient : IEventBus
    {
        public async Task Publish<TEvent>(TEvent @event) where TEvent : IIntegrationEvent
        {
            await InMemoryEventBus.Instance.Publish(@event);
        }

        public void Subscribe<TEvent>(IIntegrationEventHandler<TEvent> handler) where TEvent : IIntegrationEvent
        {
            InMemoryEventBus.Instance.Subscribe(handler);
        }
    }
}