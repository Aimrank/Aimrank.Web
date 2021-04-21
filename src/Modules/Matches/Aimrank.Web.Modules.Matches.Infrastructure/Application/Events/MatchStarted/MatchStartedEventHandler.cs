using Aimrank.Web.Modules.Matches.Application.Matches.StartMatch;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Application.Events.MatchStarted
{
    internal class MatchStartedEventHandler : INotificationHandler<MatchStartedEvent>
    {
        public Task Handle(MatchStartedEvent notification, CancellationToken cancellationToken)
            => CommandsExecutor.Execute(new StartMatchCommand(notification.MatchId));
    }
}