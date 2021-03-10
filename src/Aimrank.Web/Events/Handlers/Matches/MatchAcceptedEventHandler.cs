using Aimrank.Common.Application.Events;
using Aimrank.IntegrationEvents.Matches;
using HotChocolate.Subscriptions;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Events.Handlers.Matches
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
            foreach (var lobbyId in @event.Lobbies)
            {
                await _topicEventSender.SendAsync($"MatchAccepted:{lobbyId}", @event, cancellationToken);
            }
        }
    }
}