using System;

namespace Aimrank.Web.Contracts.Friendships
{
    public class BlockUserRequest
    {
        public Guid BlockedUserId { get; set; }
    }
}