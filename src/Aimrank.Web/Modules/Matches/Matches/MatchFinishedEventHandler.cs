using Aimrank.Common.Application.Events;
using Aimrank.Modules.Matches.IntegrationEvents.Matches;
using Aimrank.Web.GraphQL.Subscriptions.Lobbies.Payloads;
using HotChocolate.Subscriptions;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Matches
{
    public class MatchFinishedEventHandler : IIntegrationEventHandler<MatchFinishedEvent>
    {
        private readonly ITopicEventSender _topicEventSender;

        public MatchFinishedEventHandler(ITopicEventSender topicEventSender)
        {
            _topicEventSender = topicEventSender;
        }

        public async Task HandleAsync(MatchFinishedEvent @event, CancellationToken cancellationToken = default)
        {
            var payload = new MatchFinishedPayload(new MatchFinishedRecord(
                @event.MatchId,
                @event.ScoreT,
                @event.ScoreCT));
            
            foreach (var lobbyId in @event.Lobbies)
            {
                await _topicEventSender.SendAsync($"MatchFinished:{lobbyId}", payload, cancellationToken);
            }
        }
    }
}
