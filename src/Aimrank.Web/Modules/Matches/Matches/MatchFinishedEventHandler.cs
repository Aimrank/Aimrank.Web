using Aimrank.Common.Application.Events;
using Aimrank.Modules.Matches.IntegrationEvents.Matches;
using Aimrank.Web.GraphQL.Subscriptions.Lobbies.Payloads;
using Aimrank.Web.GraphQL.Subscriptions.Lobbies;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Matches
{
    public class MatchFinishedEventHandler : IIntegrationEventHandler<MatchFinishedEvent>
    {
        private readonly LobbyEventSender _lobbyEventSender;

        public MatchFinishedEventHandler(LobbyEventSender lobbyEventSender)
        {
            _lobbyEventSender = lobbyEventSender;
        }

        public async Task HandleAsync(MatchFinishedEvent @event, CancellationToken cancellationToken = default)
        {
            var payload = new MatchFinishedPayload(new MatchFinishedRecord(
                @event.MatchId,
                @event.ScoreT,
                @event.ScoreCT));
            
            foreach (var lobbyId in @event.Lobbies)
            {
                await _lobbyEventSender.SendAsync($"MatchFinished", lobbyId, payload, cancellationToken);
            }
        }
    }
}
