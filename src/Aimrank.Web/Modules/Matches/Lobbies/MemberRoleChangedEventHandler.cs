using Aimrank.Common.Application.Events;
using Aimrank.Modules.Matches.IntegrationEvents.Lobbies;
using Aimrank.Web.GraphQL.Subscriptions.Lobbies.Payloads;
using HotChocolate.Subscriptions;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Lobbies
{
    public class MemberRoleChangedEventHandler : IIntegrationEventHandler<MemberRoleChangedEvent>
    {
        private readonly ITopicEventSender _topicEventSender;

        public MemberRoleChangedEventHandler(ITopicEventSender topicEventSender)
        {
            _topicEventSender = topicEventSender;
        }

        public async Task HandleAsync(MemberRoleChangedEvent @event, CancellationToken cancellationToken = default)
            => await _topicEventSender.SendAsync($"LobbyMemberRoleChanged:{@event.LobbyId}",
                new LobbyMemberRoleChangedPayload(new LobbyMemberRoleChangedRecord(@event.PlayerId, @event.Role)),
                cancellationToken);
    }
}