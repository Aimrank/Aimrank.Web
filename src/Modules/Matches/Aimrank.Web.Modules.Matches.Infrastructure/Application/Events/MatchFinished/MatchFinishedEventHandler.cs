using Aimrank.Web.Modules.Matches.Application.Matches.FinishMatch;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Application.Events.MatchFinished
{
    internal class MatchFinishedEventHandler : INotificationHandler<MatchFinishedEvent>
    {
        public Task Handle(MatchFinishedEvent notification, CancellationToken cancellationToken)
            => CommandsExecutor.Execute(new FinishMatchCommand(
                notification.MatchId,
                notification.Winner,
                notification.TeamTerrorists,
                notification.TeamCounterTerrorists));
    }
}