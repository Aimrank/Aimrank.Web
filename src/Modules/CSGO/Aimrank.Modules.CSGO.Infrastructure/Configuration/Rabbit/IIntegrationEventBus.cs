using Aimrank.Common.Application.Events;

namespace Aimrank.Modules.CSGO.Infrastructure.Configuration.Rabbit
{
    internal interface IIntegrationEventBus
    {
        void Publish(IIntegrationEvent @event);
        IIntegrationEventBus Subscribe<T>(string serviceName) where T : IIntegrationEvent;
        void Listen();
    }
}