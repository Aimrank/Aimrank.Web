using Aimrank.Common.Application.Events;
using Aimrank.IntegrationEvents.Matches;
using HotChocolate.Subscriptions;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Events.Handlers.Matches
{
    public class MatchTimedOutEventHandler : IIntegrationEventHandler<MatchTimedOutEvent>
    {
        private readonly ITopicEventSender _topicEventSender;

        public MatchTimedOutEventHandler(ITopicEventSender topicEventSender)
        {
            _topicEventSender = topicEventSender;
        }

        public async Task HandleAsync(MatchTimedOutEvent @event, CancellationToken cancellationToken = default)
        {
            foreach (var lobbyId in @event.Lobbies)
            {
                await _topicEventSender.SendAsync($"MatchTimedOut:{lobbyId}", @event, cancellationToken);
            }
        }
    }
}