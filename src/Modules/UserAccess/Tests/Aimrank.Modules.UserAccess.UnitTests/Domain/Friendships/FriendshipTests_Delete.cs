using Aimrank.Modules.UserAccess.Domain.Friendships;
using System.Threading.Tasks;
using Xunit;

namespace Aimrank.Modules.UserAccess.UnitTests.Domain.Friendships
{
    public class FriendshipTests_Delete : FriendshipTestsBase
    {
        [Fact]
        public async Task Delete_Deletes_Friendship_If_Friendship_Is_Not_Blocked()
        {
            var friendship = await CreateFriendshipAsync(UserId1);
            friendship.Accept(UserId2);
            
            FriendshipRepository.Add(friendship);
            
            friendship.Delete(UserId1, FriendshipRepository);

            Assert.False(await FriendshipRepository.ExistsForMembersAsync(friendship.Members));
        }
        
        [Fact]
        public async Task Delete_Deletes_Friendship_If_Friendship_Is_Not_Blocked_And_Pending()
        {
            var friendship = await CreateFriendshipAsync(UserId1);
            
            FriendshipRepository.Add(friendship);
            
            friendship.Delete(UserId1, FriendshipRepository);

            Assert.False(await FriendshipRepository.ExistsForMembersAsync(friendship.Members));
        }

        [Fact]
        public async Task Delete_Deletes_Friendship_If_Blocked_Only_By_Deleting_User()
        {
            var friendship = await CreateFriendshipAsync(null, UserId1);
            
            FriendshipRepository.Add(friendship);
            
            friendship.Delete(UserId1, FriendshipRepository);

            Assert.False(await FriendshipRepository.ExistsForMembersAsync(friendship.Members));
        }

        [Fact]
        public async Task Delete_DoesNot_Delete_Friendship_If_Blocked_By_Other_Users()
        {
            var friendship = await CreateFriendshipAsync(null, UserId1);
            friendship.Block(UserId2);
            
            FriendshipRepository.Add(friendship);
            
            friendship.Delete(UserId1, FriendshipRepository);

            Assert.True(await FriendshipRepository.ExistsForMembersAsync(friendship.Members));
            Assert.Equal(FriendshipState.Blocked, friendship.GetState());
        }
    }
}