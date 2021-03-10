using Aimrank.Common.Application.Events;
using Aimrank.IntegrationEvents.Matches;
using HotChocolate.Subscriptions;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Events.Handlers.Matches
{
    public class MatchCanceledEventHandler :  IIntegrationEventHandler<MatchCanceledEvent>
    {
        private readonly ITopicEventSender _topicEventSender;

        public MatchCanceledEventHandler(ITopicEventSender topicEventSender)
        {
            _topicEventSender = topicEventSender;
        }

        public async Task HandleAsync(MatchCanceledEvent @event, CancellationToken cancellationToken = default)
        {
            foreach (var lobbyId in @event.Lobbies)
            {
                await _topicEventSender.SendAsync($"MatchCanceled:{lobbyId}", @event, cancellationToken);
            }
        }
    }
}