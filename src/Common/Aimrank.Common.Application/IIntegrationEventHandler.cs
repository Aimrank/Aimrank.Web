using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Common.Application
{
    public interface IIntegrationEventHandler
    {
    }

    public interface IIntegrationEventHandler<in TEvent> : IIntegrationEventHandler where TEvent : IntegrationEvent
    {
        Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
    }
}