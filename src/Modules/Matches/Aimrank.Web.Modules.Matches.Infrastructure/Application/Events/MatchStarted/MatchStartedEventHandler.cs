using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.Matches.Application.Matches.StartMatch;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Application.Events.MatchStarted
{
    internal class MatchStartedEventHandler : IIntegrationEventHandler<MatchStartedEvent>
    {
        public Task HandleAsync(MatchStartedEvent @event, CancellationToken cancellationToken = default)
            => CommandsExecutor.Execute(new StartMatchCommand(@event.MatchId));
    }
}