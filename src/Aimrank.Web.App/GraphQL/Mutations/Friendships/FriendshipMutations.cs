using Aimrank.Web.Common.Application;
using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using Aimrank.Web.Modules.UserAccess.Application.Friendships.AcceptFriendshipInvitation;
using Aimrank.Web.Modules.UserAccess.Application.Friendships.BlockUser;
using Aimrank.Web.Modules.UserAccess.Application.Friendships.DeclineFriendshipInvitation;
using Aimrank.Web.Modules.UserAccess.Application.Friendships.DeleteFriendship;
using Aimrank.Web.Modules.UserAccess.Application.Friendships.InviteUserToFriendsList;
using Aimrank.Web.Modules.UserAccess.Application.Friendships.UnblockUser;
using Aimrank.Web.App.GraphQL.Subscriptions.Users.Payloads;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using HotChocolate;
using System.Threading.Tasks;

namespace Aimrank.Web.App.GraphQL.Mutations.Friendships
{
    [ExtendObjectType("Mutation")]
    public class FriendshipMutations
    {
        private readonly IUserAccessModule _userAccessModule;
        private readonly ITopicEventSender _sender;
        private readonly IExecutionContextAccessor _executionContextAccessor;

        public FriendshipMutations(
            IUserAccessModule userAccessModule,
            ITopicEventSender sender,
            IExecutionContextAccessor executionContextAccessor)
        {
            _userAccessModule = userAccessModule;
            _sender = sender;
            _executionContextAccessor = executionContextAccessor;
        }

        [Authorize]
        public async Task<InviteUserToFriendsListPayload> InviteUserToFriendsList(
            [GraphQLNonNullType] InviteUserToFriendsListCommand input)
        {
            await _userAccessModule.ExecuteCommandAsync(input);

            await _sender.SendAsync($"FriendshipInvitationCreated:{input.InvitedUserId}",
                new FriendshipInvitationCreatedPayload(
                    new FriendshipInvitationCreatedRecord(_executionContextAccessor.UserId)));

            return new InviteUserToFriendsListPayload();
        }

        [Authorize]
        public async Task<AcceptFriendshipInvitationPayload> AcceptFriendshipInvitation(
            [GraphQLNonNullType] AcceptFriendshipInvitationCommand input)
        {
            await _userAccessModule.ExecuteCommandAsync(input);
            return new AcceptFriendshipInvitationPayload();
        }

        [Authorize]
        public async Task<DeclineFriendshipInvitationPayload> DeclineFriendshipInvitation(
            [GraphQLNonNullType] DeclineFriendshipInvitationCommand input)
        {
            await _userAccessModule.ExecuteCommandAsync(input);
            return new DeclineFriendshipInvitationPayload();
        }

        [Authorize]
        public async Task<BlockUserPayload> BlockUser(
            [GraphQLNonNullType] BlockUserCommand input)
        {
            await _userAccessModule.ExecuteCommandAsync(input);
            return new BlockUserPayload();
        }

        [Authorize]
        public async Task<UnblockUserPayload> UnblockUser(
            [GraphQLNonNullType] UnblockUserCommand input)
        {
            await _userAccessModule.ExecuteCommandAsync(input);
            return new UnblockUserPayload();
        }

        [Authorize]
        public async Task<DeleteFriendshipPayload> DeleteFriendship(
            [GraphQLNonNullType] DeleteFriendshipCommand input)
        {
            await _userAccessModule.ExecuteCommandAsync(input);
            return new DeleteFriendshipPayload();
        }
    }
}