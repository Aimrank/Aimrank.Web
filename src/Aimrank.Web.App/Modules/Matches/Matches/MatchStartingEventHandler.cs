using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.Matches.IntegrationEvents.Matches;
using Aimrank.Web.App.GraphQL.Subscriptions.Lobbies.Payloads;
using Aimrank.Web.App.GraphQL.Subscriptions.Lobbies;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.App.Modules.Matches.Matches
{
    public class MatchStartingEventHandler : IIntegrationEventHandler<MatchStartingEvent>
    {
        private readonly LobbyEventSender _lobbyEventSender;

        public MatchStartingEventHandler(LobbyEventSender lobbyEventSender)
        {
            _lobbyEventSender = lobbyEventSender;
        }

        public async Task HandleAsync(MatchStartingEvent @event, CancellationToken cancellationToken = default)
        {
            var payload = new MatchStartingPayload(new MatchStartingRecord(@event.MatchId));
            
            foreach (var lobbyId in @event.Lobbies)
            {
                await _lobbyEventSender.SendAsync($"MatchStarting", lobbyId, payload, cancellationToken);
            }
        }
    }
}