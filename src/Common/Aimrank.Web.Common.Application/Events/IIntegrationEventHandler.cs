using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Common.Application.Events
{
    public interface IIntegrationEventHandler
    {
    }
    
    public interface IIntegrationEventHandler<in TEvent> : IIntegrationEventHandler where TEvent : class, IIntegrationEvent
    {
        Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
    }
}