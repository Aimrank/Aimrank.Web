using Aimrank.Web.App.GraphQL.Subscriptions.Lobbies.Payloads;
using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.Matches.IntegrationEvents.Matches;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.App.GraphQL.Subscriptions.Lobbies.Events
{
    public class MatchAcceptedEventHandler : IIntegrationEventHandler<MatchAcceptedEvent>
    {
        private readonly LobbyEventSender _lobbyEventSender;

        public MatchAcceptedEventHandler(LobbyEventSender lobbyEventSender)
        {
            _lobbyEventSender = lobbyEventSender;
        }

        public async Task HandleAsync(MatchAcceptedEvent @event, CancellationToken cancellationToken = default)
        {
            var payload = new MatchAcceptedPayload(new MatchAcceptedRecord(@event.MatchId, @event.PlayerId));
            
            foreach (var lobbyId in @event.Lobbies)
            {
                await _lobbyEventSender.SendAsync($"MatchAccepted", lobbyId, payload, cancellationToken);
            }
        }
    }
}