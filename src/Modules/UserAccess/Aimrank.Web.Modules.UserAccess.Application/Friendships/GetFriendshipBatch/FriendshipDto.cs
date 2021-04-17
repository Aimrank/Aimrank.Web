using System.Collections.Generic;
using System;

namespace Aimrank.Web.Modules.UserAccess.Application.Friendships.GetFriendshipBatch
{
    public class FriendshipDto
    {
        public Guid UserId1 { get; set; }
        public Guid UserId2 { get; set; }
        public Guid? InvitingUserId { get; set; }
        public IEnumerable<Guid> BlockingUsersIds { get; set; }
        public bool IsAccepted { get; set; }
    }
}