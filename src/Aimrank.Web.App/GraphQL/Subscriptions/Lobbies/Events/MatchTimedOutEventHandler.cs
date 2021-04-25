using Aimrank.Web.App.GraphQL.Subscriptions.Lobbies.Payloads;
using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.Matches.IntegrationEvents.Matches;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.App.GraphQL.Subscriptions.Lobbies.Events
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