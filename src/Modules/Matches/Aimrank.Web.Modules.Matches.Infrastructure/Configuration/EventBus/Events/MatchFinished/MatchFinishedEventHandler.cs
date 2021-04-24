using Aimrank.Web.Modules.Matches.Application.Matches.FinishMatch;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration.EventBus.Events.MatchFinished
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