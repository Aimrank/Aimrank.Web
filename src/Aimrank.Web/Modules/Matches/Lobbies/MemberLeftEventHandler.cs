using Aimrank.Common.Application.Events;
using Aimrank.Modules.Matches.IntegrationEvents.Lobbies;
using Aimrank.Web.GraphQL.Subscriptions.Lobbies.Payloads;
using HotChocolate.Subscriptions;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Lobbies
{
    public class MemberLeftEventHandler : IIntegrationEventHandler<MemberLeftEvent>
    {
        private readonly ITopicEventSender _topicEventSender;

        public MemberLeftEventHandler(ITopicEventSender topicEventSender)
        {
            _topicEventSender = topicEventSender;
        }

        public async Task HandleAsync(MemberLeftEvent @event, CancellationToken cancellationToken = default)
            => await _topicEventSender.SendAsync($"LobbyMemberLeft:{@event.LobbyId}",
                new LobbyMemberLeftPayload(new LobbyMemberLeftRecord(@event.LobbyId, @event.PlayerId)),
                cancellationToken);
    }
}