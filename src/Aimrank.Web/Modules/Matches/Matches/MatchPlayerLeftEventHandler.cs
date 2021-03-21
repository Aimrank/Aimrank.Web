using Aimrank.Common.Application.Events;
using Aimrank.Modules.Matches.IntegrationEvents.Matches;
using Aimrank.Web.GraphQL.Subscriptions.Lobbies.Payloads;
using Aimrank.Web.GraphQL.Subscriptions.Lobbies;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Matches
{
    public class MatchPlayerLeftEventHandler : IIntegrationEventHandler<MatchPlayerLeftEvent>
    {
        private readonly LobbyEventSender _lobbyEventSender;

        public MatchPlayerLeftEventHandler(LobbyEventSender lobbyEventSender)
        {
            _lobbyEventSender = lobbyEventSender;
        }

        public async Task HandleAsync(MatchPlayerLeftEvent @event, CancellationToken cancellationToken = default)
        {
            var payload = new MatchPlayerLeftPayload(new MatchPlayerLeftRecord(@event.PlayerId));
            
            foreach (var lobbyId in @event.Lobbies)
            {
                await _lobbyEventSender.SendAsync($"MatchPlayerLeft", lobbyId, payload, cancellationToken);
            }
        }
    }
}