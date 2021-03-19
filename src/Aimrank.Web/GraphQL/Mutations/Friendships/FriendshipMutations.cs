using Aimrank.Common.Application;
using Aimrank.Modules.UserAccess.Application.Contracts;
using Aimrank.Modules.UserAccess.Application.Friendships.AcceptFriendshipInvitation;
using Aimrank.Modules.UserAccess.Application.Friendships.BlockUser;
using Aimrank.Modules.UserAccess.Application.Friendships.DeclineFriendshipInvitation;
using Aimrank.Modules.UserAccess.Application.Friendships.DeleteFriendship;
using Aimrank.Modules.UserAccess.Application.Friendships.InviteUserToFriendsList;
using Aimrank.Modules.UserAccess.Application.Friendships.UnblockUser;
using Aimrank.Web.GraphQL.Subscriptions.Users;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using System.Threading.Tasks;

namespace Aimrank.Web.GraphQL.Mutations.Friendships
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
        public async Task<InviteUserToFriendsListPayload> InviteUserToFriendsList(InviteUserToFriendsListCommand command)
        {
            await _userAccessModule.ExecuteCommandAsync(command);

            await _sender.SendAsync($"FriendshipInvitationCreated:{command.InvitedUserId}",
                new FriendshipInvitationCreatedPayload(
                    new FriendshipInvitationCreatedRecord(_executionContextAccessor.UserId, command.InvitedUserId)));

            return new InviteUserToFriendsListPayload();
        }

        [Authorize]
        public async Task<AcceptFriendshipInvitationPayload> AcceptFriendshipInvitation(
            AcceptFriendshipInvitationCommand command)
        {
            await _userAccessModule.ExecuteCommandAsync(command);
            return new AcceptFriendshipInvitationPayload();
        }

        [Authorize]
        public async Task<DeclineFriendshipInvitationPayload> DeclineFriendshipInvitation(
            DeclineFriendshipInvitationCommand command)
        {
            await _userAccessModule.ExecuteCommandAsync(command);
            return new DeclineFriendshipInvitationPayload();
        }

        [Authorize]
        public async Task<BlockUserPayload> BlockUser(BlockUserCommand command)
        {
            await _userAccessModule.ExecuteCommandAsync(command);
            return new BlockUserPayload();
        }

        [Authorize]
        public async Task<UnblockUserPayload> UnblockUser(UnblockUserCommand command)
        {
            await _userAccessModule.ExecuteCommandAsync(command);
            return new UnblockUserPayload();
        }

        [Authorize]
        public async Task<DeleteFriendshipPayload> DeleteFriendship(DeleteFriendshipCommand command)
        {
            await _userAccessModule.ExecuteCommandAsync(command);
            return new DeleteFriendshipPayload();
        }
    }
}