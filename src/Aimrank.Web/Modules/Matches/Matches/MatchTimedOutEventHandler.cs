using Aimrank.Common.Application.Events;
using Aimrank.Modules.Matches.IntegrationEvents.Matches;
using Aimrank.Web.GraphQL.Subscriptions.Lobbies.Payloads;
using Aimrank.Web.GraphQL.Subscriptions.Lobbies;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Matches
{
    public class MatchTimedOutEventHandler : IIntegrationEventHandler<MatchTimedOutEvent>
    {
        private readonly LobbyEventSender _lobbyEventSender;

        public MatchTimedOutEventHandler(LobbyEventSender lobbyEventSender)
        {
            _lobbyEventSender = lobbyEventSender;
        }

        public async Task HandleAsync(MatchTimedOutEvent @event, CancellationToken cancellationToken = default)
        {
            var payload = new MatchTimedOutPayload(new MatchTimedOutRecord(@event.MatchId));
            
            foreach (var lobbyId in @event.Lobbies)
            {
                await _lobbyEventSender.SendAsync($"MatchTimedOut", lobbyId, payload, cancellationToken);
            }
        }
    }
}