using Aimrank.Common.Application.Events;
using Aimrank.Modules.Matches.IntegrationEvents.Matches;
using Aimrank.Web.GraphQL.Subscriptions.Lobbies.Payloads;
using HotChocolate.Subscriptions;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Matches
{
    public class MatchAcceptedEventHandler : IIntegrationEventHandler<MatchAcceptedEvent>
    {
        private readonly ITopicEventSender _topicEventSender;

        public MatchAcceptedEventHandler(ITopicEventSender topicEventSender)
        {
            _topicEventSender = topicEventSender;
        }

        public async Task HandleAsync(MatchAcceptedEvent @event, CancellationToken cancellationToken = default)
        {
            var payload = new MatchAcceptedPayload(new MatchAcceptedRecord(@event.MatchId, @event.PlayerId));
            
            foreach (var lobbyId in @event.Lobbies)
            {
                await _topicEventSender.SendAsync($"MatchAccepted:{lobbyId}", payload, cancellationToken);
            }
        }
    }
}