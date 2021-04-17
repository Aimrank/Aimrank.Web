using Aimrank.Web.Common.Application.Events;
using System.Threading.Tasks;

namespace Aimrank.Web.Common.Infrastructure.EventBus
{
    public interface IEventBus
    {
        public Task Publish<TEvent>(TEvent @event) where TEvent : IIntegrationEvent;
        public void Subscribe<TEvent>(IIntegrationEventHandler<TEvent> handler) where TEvent : IIntegrationEvent;
    }
}