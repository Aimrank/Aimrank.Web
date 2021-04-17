using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.Matches.Application.Matches.FinishMatch;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Application.Events.MatchFinished
{
    internal class MatchFinishedEventHandler : IIntegrationEventHandler<MatchFinishedEvent>
    {
        public Task HandleAsync(MatchFinishedEvent @event, CancellationToken cancellationToken = default)
            => CommandsExecutor.Execute(new FinishMatchCommand(
                @event.MatchId,
                @event.Winner,
                @event.TeamTerrorists,
                @event.TeamCounterTerrorists));
    }
}