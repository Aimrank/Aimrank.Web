using Aimrank.Modules.UserAccess.Domain.Friendships;
using Aimrank.Modules.UserAccess.Domain.Users;
using System;
using Xunit;

namespace Aimrank.Modules.UserAccess.UnitTests.Domain.Friendships
{
    public class FriendshipMembersTests
    {
        [Fact]
        public void FriendshipMembers_Is_The_Same_For_Opposite_Combinations()
        {
            var u1 = new UserId(Guid.NewGuid());
            var u2 = new UserId(Guid.NewGuid());

            var members1 = new FriendshipMembers(u1, u2);
            var members2 = new FriendshipMembers(u2, u1);
            
            Assert.Equal(members1, members2);
        }
    }
}