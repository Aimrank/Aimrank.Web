using Aimrank.Application.Commands.Friendships.AcceptFriendshipInvitation;
using Aimrank.Application.Commands.Friendships.BlockUser;
using Aimrank.Application.Commands.Friendships.DeclineFriendshipInvitation;
using Aimrank.Application.Commands.Friendships.DeleteFriendship;
using Aimrank.Application.Commands.Friendships.InviteUserToFriendsList;
using Aimrank.Application.Commands.Friendships.UnblockUser;
using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.Friendships.GetBlockedUsers;
using Aimrank.Application.Queries.Friendships.GetFriendsList;
using Aimrank.Application.Queries.Friendships.GetFriendship;
using Aimrank.Application.Queries.Friendships.GetFriendshipInvitations;
using Aimrank.Application.Queries.Users.GetUserDetails;
using Aimrank.Web.Attributes;
using Aimrank.Web.Contracts.Friendships;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.Controllers
{
    [JwtAuth]
    [ApiController]
    [Route("api/[controller]")]
    public class FriendshipController : ControllerBase
    {
        private readonly IAimrankModule _aimrankModule;

        public FriendshipController(IAimrankModule aimrankModule)
        {
            _aimrankModule = aimrankModule;
        }

        [HttpGet("{userId1}/{userId2}")]
        public async Task<ActionResult<FriendshipDto>> GetFriendship(Guid userId1, Guid userId2)
        {
            var friendship = await _aimrankModule.ExecuteQueryAsync(new GetFriendshipQuery(userId1, userId2));
            if (friendship is null)
            {
                return NoContent();
            }

            return friendship;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetFriends(Guid userId)
            => Ok(await _aimrankModule.ExecuteQueryAsync(new GetFriendsListQuery(userId)));

        [HttpGet("invitations")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetInvitations()
            => Ok(await _aimrankModule.ExecuteQueryAsync(new GetFriendshipInvitationsQuery()));

        [HttpGet("blocked")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetBlockedUsers()
            => Ok(await _aimrankModule.ExecuteQueryAsync(new GetBlockedUsersQuery()));
        
        [HttpPost("invite")]
        public async Task<IActionResult> CreateInvitation(CreateFriendshipInvitationRequest request)
        {
            await _aimrankModule.ExecuteCommandAsync(new InviteUserToFriendsListCommand(request.InvitedUserId));
            return Ok();
        }

        [HttpPost("accept")]
        public async Task<IActionResult> AcceptInvitation(AcceptFriendshipInvitationRequest request)
        {
            await _aimrankModule.ExecuteCommandAsync(new AcceptFriendshipInvitationCommand(request.InvitingUserId));
            return Ok();
        }

        [HttpPost("decline")]
        public async Task<IActionResult> DeclineInvitation(DeclineFriendshipInvitationRequest request)
        {
            await _aimrankModule.ExecuteCommandAsync(new DeclineFriendshipInvitationCommand(request.InvitingUserId));
            return Ok();
        }

        [HttpPost("block")]
        public async Task<IActionResult> BlockUser(BlockUserRequest request)
        {
            await _aimrankModule.ExecuteCommandAsync(new BlockUserCommand(request.BlockedUserId));
            return Ok();
        }

        [HttpPost("unblock")]
        public async Task<IActionResult> UnblockUser(UnblockUserRequest request)
        {
            await _aimrankModule.ExecuteCommandAsync(new UnblockUserCommand(request.BlockedUserId));
            return Ok();
        }
        
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteFriend(Guid userId)
        {
            await _aimrankModule.ExecuteCommandAsync(new DeleteFriendshipCommand(userId));
            return Ok();
        }
    }
}