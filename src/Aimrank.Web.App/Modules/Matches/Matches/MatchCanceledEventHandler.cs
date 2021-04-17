using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.Matches.IntegrationEvents.Matches;
using Aimrank.Web.App.GraphQL.Subscriptions.Lobbies.Payloads;
using Aimrank.Web.App.GraphQL.Subscriptions.Lobbies;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.App.Modules.Matches.Matches
{
    public class MatchCanceledEventHandler :  IIntegrationEventHandler<MatchCanceledEvent>
    {
        private readonly LobbyEventSender _lobbyEventSender;

        public MatchCanceledEventHandler(LobbyEventSender lobbyEventSender)
        {
            _lobbyEventSender = lobbyEventSender;
        }

        public async Task HandleAsync(MatchCanceledEvent @event, CancellationToken cancellationToken = default)
        {
            var payload = new MatchCanceledPayload(new MatchCanceledRecord(@event.MatchId));
            
            foreach (var lobbyId in @event.Lobbies)
            {
                await _lobbyEventSender.SendAsync($"MatchCanceled", lobbyId, payload, cancellationToken);
            }
        }
    }
}