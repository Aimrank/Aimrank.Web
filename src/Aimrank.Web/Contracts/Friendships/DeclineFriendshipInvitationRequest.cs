using System;

namespace Aimrank.Web.Contracts.Friendships
{
    public class DeclineFriendshipInvitationRequest
    {
        public Guid InvitingUserId { get; set; }
    }
}