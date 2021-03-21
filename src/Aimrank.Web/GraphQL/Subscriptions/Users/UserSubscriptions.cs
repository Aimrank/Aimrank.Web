using Aimrank.Web.GraphQL.Subscriptions.Users.Payloads;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using System.Security.Claims;
using System.Threading.Tasks;

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
            => _receiver.SubscribeAsync<string, LobbyInvitationCreatedPayload>(
                $"LobbyInvitationCreated:{principal.GetUserId()}");

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<FriendshipInvitationCreatedPayload>> FriendshipInvitationCreated(
            [ClaimsPrincipal] ClaimsPrincipal principal)
            => _receiver.SubscribeAsync<string, FriendshipInvitationCreatedPayload>(
                $"FriendshipInvitationCreated:{principal.GetUserId()}");
    }
}