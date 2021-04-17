using Aimrank.Common.Application.Events;
using Aimrank.Modules.Matches.Application.Matches.CancelMatch;
using Aimrank.Modules.Matches.Infrastructure.Configuration;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Modules.Matches.Infrastructure.Application.Events.MatchCanceled
{
    internal class MatchCanceledEventHandler : IIntegrationEventHandler<MatchCanceledEvent>
    {
        public Task HandleAsync(MatchCanceledEvent @event, CancellationToken cancellationToken = default)
            => CommandsExecutor.Execute(new CancelMatchCommand(@event.MatchId));
    }
}