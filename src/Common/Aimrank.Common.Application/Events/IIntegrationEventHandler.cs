using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Common.Application.Events
{
    public interface IIntegrationEventHandler
    {
    }

    public interface IIntegrationEventHandler<in TEvent> : IIntegrationEventHandler where TEvent : IIntegrationEvent
    {
        Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
    }
}