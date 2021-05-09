using Aimrank.Web.Modules.Matches.Application.Matches.MarkPlayerAsLeaver;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Events.Events.PlayerDisconnected
{
    internal class PlayerDisconnectedEventHandler : INotificationHandler<PlayerDisconnectedEvent>
    {
        public Task Handle(PlayerDisconnectedEvent notification, CancellationToken cancellationToken)
            => CommandsExecutor.Execute(new MarkPlayerAsLeaverCommand(notification.MatchId, notification.SteamId));
    }
}