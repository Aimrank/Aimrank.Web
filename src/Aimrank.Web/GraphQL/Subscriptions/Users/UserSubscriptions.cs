using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using System.Security.Claims;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.GraphQL.Subscriptions.Users
{
    [ExtendObjectType("Subscription")]
    public class UserSubscriptions : AuthenticatedSubscriptions
    {
        private readonly ITopicEventReceiver _receiver;
        
        public UserSubscriptions(ITopicEventReceiver receiver)
        {
            _receiver = receiver;
        }

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<LobbyInvitationCreatedPayload>> LobbyInvitationCreated(
            Guid playerId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
        {
            AssertAuthenticated(principal, playerId);

            return _receiver.SubscribeAsync<string, LobbyInvitationCreatedPayload>(
                $"LobbyInvitationCreated:{playerId}");
        }

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<FriendshipInvitationCreatedPayload>> FriendshipInvitationCreated(
            Guid userId,
            [ClaimsPrincipal] ClaimsPrincipal principal)
        {
            AssertAuthenticated(principal, userId);
            
            return _receiver.SubscribeAsync<string, FriendshipInvitationCreatedPayload>(
                $"FriendshipInvitationCreated:{userId}");
        }
    }
}