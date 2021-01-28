using Aimrank.Common.Application;
using System.Threading.Tasks;

namespace Aimrank.Common.Infrastructure.EventBus
{
    public interface IEventBus
    {
        public Task Publish<TEvent>(TEvent @event) where TEvent : IntegrationEvent;
        public void Subscribe<TEvent>(IIntegrationEventHandler<TEvent> handler) where TEvent : IntegrationEvent;
    }
}