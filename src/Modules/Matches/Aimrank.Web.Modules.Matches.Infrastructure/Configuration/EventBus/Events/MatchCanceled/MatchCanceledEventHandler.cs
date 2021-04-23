using Aimrank.Web.Modules.Matches.Application.Matches.CancelMatch;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration.EventBus.Events.MatchCanceled
{
    internal class MatchCanceledEventHandler : INotificationHandler<MatchCanceledEvent>
    {
        public Task Handle(MatchCanceledEvent notification, CancellationToken cancellationToken)
            => CommandsExecutor.Execute(new CancelMatchCommand(notification.MatchId));
    }
}