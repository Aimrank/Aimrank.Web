using Aimrank.Web.Modules.Matches.Application.Matches.CancelMatch;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Application.Events.MatchCanceled
{
    internal class MatchCanceledEventHandler : INotificationHandler<MatchCanceledEvent>
    {
        public Task Handle(MatchCanceledEvent notification, CancellationToken cancellationToken)
            => CommandsExecutor.Execute(new CancelMatchCommand(notification.MatchId));
    }
}