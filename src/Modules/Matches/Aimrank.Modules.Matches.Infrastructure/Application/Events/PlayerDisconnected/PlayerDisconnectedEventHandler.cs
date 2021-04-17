using Aimrank.Common.Application.Events;
using Aimrank.Modules.Matches.Application.Matches.MarkPlayerAsLeaver;
using Aimrank.Modules.Matches.Infrastructure.Configuration;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Modules.Matches.Infrastructure.Application.Events.PlayerDisconnected
{
    internal class PlayerDisconnectedEventHandler : IIntegrationEventHandler<PlayerDisconnectedEvent>
    {
        public Task HandleAsync(PlayerDisconnectedEvent @event, CancellationToken cancellationToken = default)
            => CommandsExecutor.Execute(new MarkPlayerAsLeaverCommand(@event.MatchId, @event.SteamId));
    }
}