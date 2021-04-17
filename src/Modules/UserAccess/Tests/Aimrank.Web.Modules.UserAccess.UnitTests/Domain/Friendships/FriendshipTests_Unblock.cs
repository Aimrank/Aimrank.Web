using Aimrank.Web.Modules.UserAccess.Domain.Friendships;
using System.Threading.Tasks;
using Xunit;

namespace Aimrank.Web.Modules.UserAccess.UnitTests.Domain.Friendships
{
    public class FriendshipTests_Unblock : FriendshipTestsBase
    {
        [Fact]
        public async Task Unblock_Deletes_Friendship_If_Blocked_By_Unblocking_User()
        {
            var friendship = await CreateFriendshipAsync(null, UserId1);
            
            FriendshipRepository.Add(friendship);
            
            friendship.Unblock(UserId1, FriendshipRepository);
            
            Assert.False(await FriendshipRepository.ExistsForMembersAsync(friendship.Members));
        }

        [Fact]
        public async Task Unblock_DoesNot_Delete_Friendship_If_Other_Users_Are_Blocking()
        {
            var friendship = await CreateFriendshipAsync(null, UserId1);
            friendship.Block(UserId2);
            
            FriendshipRepository.Add(friendship);

            friendship.Unblock(UserId1, FriendshipRepository);
            
            Assert.True(await FriendshipRepository.ExistsForMembersAsync(friendship.Members));
        }

        [Fact]
        public async Task Unblock_DoesNot_Delete_Friendship_If_Friendship_Is_Accepted()
        {
            var friendship = await CreateFriendshipAsync(UserId1);
            friendship.Accept(UserId2);
            friendship.Block(UserId2);
            
            FriendshipRepository.Add(friendship);
            
            friendship.Unblock(UserId2, FriendshipRepository);
            
            Assert.True(await FriendshipRepository.ExistsForMembersAsync(friendship.Members));
            Assert.Equal(FriendshipState.Active, friendship.GetState());
        }
    }
}