using Aimrank.Web.GraphQL.Subscriptions.Users.Payloads;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using System.Security.Claims;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.GraphQL.Subscriptions.Users
{
    [ExtendObjectType("Subscription")]
    public class UserSubscriptions
    {
        private readonly ITopicEventReceiver _receiver;
        
        public UserSubscriptions(ITopicEventReceiver receiver)
        {
            _receiver = receiver;
        }

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<LobbyInvitationCreatedPayload>> LobbyInvitationCreated(
            [ClaimsPrincipal] ClaimsPrincipal principal)
            => SubscribeAuthenticated<LobbyInvitationCreatedPayload>(
                $"LobbyInvitationCreated:{principal.GetUserId()}", principal);

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<FriendshipInvitationCreatedPayload>> FriendshipInvitationCreated(
            [ClaimsPrincipal] ClaimsPrincipal principal)
            => SubscribeAuthenticated<FriendshipInvitationCreatedPayload>(
                $"FriendshipInvitationCreated:{principal.GetUserId()}", principal);
        
        private ValueTask<ISourceStream<TMessage>> SubscribeAuthenticated<TMessage>(string topic,
            ClaimsPrincipal principal)
            => principal.GetUserId() == Guid.Empty
                ? new ValueTask<ISourceStream<TMessage>>(new EmptySourceStream<TMessage>())
                : _receiver.SubscribeAsync<string, TMessage>(topic);
    }
}