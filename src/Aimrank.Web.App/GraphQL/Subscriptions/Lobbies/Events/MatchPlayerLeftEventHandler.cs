using Aimrank.Web.App.GraphQL.Subscriptions.Lobbies.Payloads;
using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.Matches.IntegrationEvents.Matches;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.App.GraphQL.Subscriptions.Lobbies.Events
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