using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Common.Domain
{
    public interface IDomainEventHandler
    {
    }

    public interface IDomainEventHandler<in TEvent> : IDomainEventHandler where TEvent : class, IDomainEvent
    {
        Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
    }
}