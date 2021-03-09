using Aimrank.Application.Queries.Users.GetUserDetails;
using System.Collections.Generic;
using System;

namespace Aimrank.Application.Queries.Friendships.GetFriendship
{
    public class FriendshipDto
    {
        public UserDto User1 { get; set; }
        public UserDto User2 { get; set; }
        public Guid? InvitingUserId { get; set; }
        public IEnumerable<Guid> BlockingUsersIds { get; set; }
        public bool IsAccepted { get; set; }
    }
}