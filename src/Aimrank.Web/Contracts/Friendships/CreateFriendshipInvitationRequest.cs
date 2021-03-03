using System;

namespace Aimrank.Web.Contracts.Friendships
{
    public class CreateFriendshipInvitationRequest
    {
        public Guid InvitedUserId { get; set; }
    }
}