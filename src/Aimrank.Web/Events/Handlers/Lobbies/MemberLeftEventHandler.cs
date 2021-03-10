using Aimrank.Common.Application.Events;
using Aimrank.IntegrationEvents.Lobbies;
using HotChocolate.Subscriptions;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Events.Handlers.Lobbies
{
    public class MemberLeftEventHandler : IIntegrationEventHandler<MemberLeftEvent>
    {
        private readonly ITopicEventSender _topicEventSender;

        public MemberLeftEventHandler(ITopicEventSender topicEventSender)
        {
            _topicEventSender = topicEventSender;
        }

        public async Task HandleAsync(MemberLeftEvent @event, CancellationToken cancellationToken = default)
            => await _topicEventSender.SendAsync($"LobbyMemberLeft:{@event.LobbyId}", @event, cancellationToken);
    }
}