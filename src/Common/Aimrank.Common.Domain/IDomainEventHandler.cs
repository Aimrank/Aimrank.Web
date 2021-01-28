using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Common.Domain
{
    public interface IDomainEventHandler
    {
    }

    public interface IDomainEventHandler<in TEvent> : IDomainEventHandler where TEvent : class, IDomainEventHandler
    {
        Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
    }
}