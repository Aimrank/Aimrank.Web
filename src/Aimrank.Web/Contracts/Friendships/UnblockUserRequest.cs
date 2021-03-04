using System;

namespace Aimrank.Web.Contracts.Friendships
{
    public class UnblockUserRequest
    {
        public Guid BlockedUserId { get; set; }
    }
}