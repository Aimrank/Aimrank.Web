using Aimrank.Common.Application.Events;
using Aimrank.Modules.Matches.IntegrationEvents.Matches;
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
            foreach (var lobbyId in @event.Lobbies)
            {
                await _topicEventSender.SendAsync($"MatchStarted:{lobbyId}", @event, cancellationToken);
            }
        }
    }
}