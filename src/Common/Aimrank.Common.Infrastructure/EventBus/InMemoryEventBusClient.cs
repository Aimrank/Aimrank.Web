using Aimrank.Common.Application;
using System.Threading.Tasks;

namespace Aimrank.Common.Infrastructure.EventBus
{
    public class InMemoryEventBusClient : IEventBus
    {
        public async Task Publish<TEvent>(TEvent @event) where TEvent : IntegrationEvent
        {
            await InMemoryEventBus.Instance.Publish(@event);
        }

        public void Subscribe<TEvent>(IIntegrationEventHandler<TEvent> handler) where TEvent : IntegrationEvent
        {
            InMemoryEventBus.Instance.Subscribe(handler);
        }
    }
}