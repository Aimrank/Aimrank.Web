using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.Matches.IntegrationEvents.Matches;
using Aimrank.Web.App.GraphQL.Subscriptions.Lobbies.Payloads;
using Aimrank.Web.App.GraphQL.Subscriptions.Lobbies;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.App.Modules.Matches.Matches
{
    public class MatchStartedEventHandler : IIntegrationEventHandler<MatchStartedEvent>
    {
        private readonly LobbyEventSender _lobbyEventSender;

        public MatchStartedEventHandler(LobbyEventSender lobbyEventSender)
        {
            _lobbyEventSender = lobbyEventSender;
        }

        public async Task HandleAsync(MatchStartedEvent @event, CancellationToken cancellationToken = default)
        {
            var payload = new MatchStartedPayload(new MatchStartedRecord(
                @event.MatchId,
                @event.Map,
                @event.Address,
                @event.Mode,
                @event.Players));
            
            foreach (var lobbyId in @event.Lobbies)
            {
                await _lobbyEventSender.SendAsync($"MatchStarted", lobbyId, payload, cancellationToken);
            }
        }
    }
}