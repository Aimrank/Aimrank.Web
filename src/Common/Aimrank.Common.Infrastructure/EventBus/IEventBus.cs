using Aimrank.Common.Application.Events;
using System.Threading.Tasks;

namespace Aimrank.Common.Infrastructure.EventBus
{
    public interface IEventBus
    {
        public Task Publish<TEvent>(TEvent @event) where TEvent : IIntegrationEvent;
        public void Subscribe<TEvent>(IIntegrationEventHandler<TEvent> handler) where TEvent : IIntegrationEvent;
    }
}