using Aimrank.Modules.UserAccess.Domain.Friendships.Rules;
using System.Threading.Tasks;
using Xunit;

namespace Aimrank.Modules.UserAccess.UnitTests.Domain.Friendships
{
    public class FriendshipTests_Decline : FriendshipTestsBase
    {
        [Fact]
        public async Task Decline_Deletes_Friendship_When_Succeeded()
        {
            var friendship = await CreateFriendshipAsync(UserId1);
            
            FriendshipRepository.Add(friendship);
            
            friendship.Decline(UserId2, FriendshipRepository);
            
            Assert.False(await FriendshipRepository.ExistsForMembersAsync(friendship.Members));
        }
        
        [Fact]
        public async Task Decline_Throws_If_Friendship_Is_Not_In_Pending_State()
        {
            var friendship = await CreateFriendshipAsync(UserId1);
            
            friendship.Accept(UserId2);
            
            FriendshipRepository.Add(friendship);

            void TestCode() => friendship.Decline(UserId2, FriendshipRepository);
            
            DomainAssert.FailsBusinessRule<FriendshipMustHaveStateRule>(TestCode);
        }

        [Fact]
        public async Task Decline_Throws_If_DecliningUser_Is_Not_InvitedUser()
        {
            var friendship = await CreateFriendshipAsync(UserId1);
            
            FriendshipRepository.Add(friendship);

            void TestCode() => friendship.Decline(UserId1, FriendshipRepository);
            
            DomainAssert.FailsBusinessRule<FriendshipCanBeDeclinedOnlyByInvitedUserRule>(TestCode);
        }
    }
}