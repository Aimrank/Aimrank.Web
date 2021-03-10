using Aimrank.Common.Application.Events;
using Aimrank.IntegrationEvents.Lobbies;
using HotChocolate.Subscriptions;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Events.Handlers.Lobbies
{
    public class MemberRoleChangedEventHandler : IIntegrationEventHandler<MemberRoleChangedEvent>
    {
        private readonly ITopicEventSender _topicEventSender;

        public MemberRoleChangedEventHandler(ITopicEventSender topicEventSender)
        {
            _topicEventSender = topicEventSender;
        }

        public async Task HandleAsync(MemberRoleChangedEvent @event, CancellationToken cancellationToken = default)
            => await _topicEventSender.SendAsync($"LobbyMemberRoleChanged:{@event.LobbyId}", @event, cancellationToken);
    }
}