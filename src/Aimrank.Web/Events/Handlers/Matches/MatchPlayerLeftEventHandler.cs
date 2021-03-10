using Aimrank.Common.Application.Events;
using Aimrank.IntegrationEvents.Matches;
using HotChocolate.Subscriptions;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Events.Handlers.Matches
{
    public class MatchPlayerLeftEventHandler : IIntegrationEventHandler<MatchPlayerLeftEvent>
    {
        private readonly ITopicEventSender _topicEventSender;

        public MatchPlayerLeftEventHandler(ITopicEventSender topicEventSender)
        {
            _topicEventSender = topicEventSender;
        }

        public async Task HandleAsync(MatchPlayerLeftEvent @event, CancellationToken cancellationToken = default)
            => await _topicEventSender.SendAsync($"MatchPlayerLeft{@event.UserId}", @event, cancellationToken);
    }
}