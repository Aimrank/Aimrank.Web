using Aimrank.Common.Application.Events;
using Aimrank.Modules.Matches.IntegrationEvents.Matches;
using Aimrank.Web.GraphQL.Subscriptions.Lobbies.Payloads;
using HotChocolate.Subscriptions;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Matches
{
    public class MatchStartedEventHandler : IIntegrationEventHandler<MatchStartedEvent>
    {
        private readonly ITopicEventSender _topicEventSender;

        public MatchStartedEventHandler(ITopicEventSender topicEventSender)
        {
            _topicEventSender = topicEventSender;
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
                await _topicEventSender.SendAsync($"MatchStarted:{lobbyId}", payload, cancellationToken);
            }
        }
    }
}