using Aimrank.Web.Common.Application.Events;

namespace Aimrank.Web.Common.Infrastructure.EventBus.RabbitMQ
{
    public interface IRabbitMQEventBus
    {
        void Publish(IIntegrationEvent @event);
        void Subscribe<T>() where T : IIntegrationEvent;
        void Listen();
    }
}