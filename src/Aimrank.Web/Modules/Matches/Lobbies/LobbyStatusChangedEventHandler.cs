using Aimrank.Common.Application.Events;
using Aimrank.Modules.Matches.IntegrationEvents.Lobbies;
using Aimrank.Web.GraphQL.Subscriptions.Lobbies.Payloads;
using Aimrank.Web.GraphQL.Subscriptions.Lobbies;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Lobbies
{
    public class LobbyStatusChangedEventHandler : IIntegrationEventHandler<LobbyStatusChangedEvent>
    {
        private readonly LobbyEventSender _lobbyEventSender;

        public LobbyStatusChangedEventHandler(LobbyEventSender lobbyEventSender)
        {
            _lobbyEventSender = lobbyEventSender;
        }

        public async Task HandleAsync(LobbyStatusChangedEvent @event, CancellationToken cancellationToken = default)
            => await _lobbyEventSender.SendAsync($"LobbyStatusChanged", @event.LobbyId,
                new LobbyStatusChangedPayload(new LobbyStatusChangedRecord(@event.LobbyId, @event.Status)),
                cancellationToken);
    }
}