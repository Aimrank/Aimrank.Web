using System;

namespace Aimrank.Web.Contracts.Friendships
{
    public class AcceptFriendshipInvitationRequest
    {
        public Guid InvitingUserId { get; set; }
    }
}