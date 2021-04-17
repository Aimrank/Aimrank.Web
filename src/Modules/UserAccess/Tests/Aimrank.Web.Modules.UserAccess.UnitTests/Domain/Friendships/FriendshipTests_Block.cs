using Aimrank.Web.Modules.UserAccess.Domain.Friendships.Rules;
using Aimrank.Web.Modules.UserAccess.Domain.Friendships;
using System.Threading.Tasks;
using Xunit;

namespace Aimrank.Web.Modules.UserAccess.UnitTests.Domain.Friendships
{
    public class FriendshipTests_Block : FriendshipTestsBase
    {
        [Fact]
        public async Task Block_Changes_Friendship_State_When_Succeeded()
        {
            var friendship1 = await CreateFriendshipAsync(UserId1);
            friendship1.Accept(UserId2);
            friendship1.Block(UserId1);
            
            var friendship2 = await CreateFriendshipAsync(UserId1);
            friendship2.Block(UserId2);

            var friendship3 = await CreateFriendshipAsync(UserId1);
            friendship3.Block(UserId1);
            
            Assert.Equal(FriendshipState.Blocked, friendship1.GetState());
            Assert.Equal(FriendshipState.Blocked, friendship2.GetState());
            Assert.Equal(FriendshipState.Blocked, friendship3.GetState());
        }

        [Fact]
        public async Task Block_DoesNot_Change_Friendship_State_When_Succeeded_And_Already_Blocked()
        {
            var friendship = await CreateFriendshipAsync(UserId1);
            
            friendship.Accept(UserId2);
            friendship.Block(UserId1);
            friendship.Block(UserId2);
            
            Assert.Equal(FriendshipState.Blocked, friendship.GetState());
        }

        [Fact]
        public async Task Block_Throws_When_User_Is_Already_Blocking_Friendship()
        {
            var friendship = await CreateFriendshipAsync(UserId1);
            
            friendship.Accept(UserId2);
            friendship.Block(UserId1);

            void TestCode() => friendship.Block(UserId1);
            
            DomainAssert.FailsBusinessRule<FriendshipMustNotBeBlockedByUserRule>(TestCode);
        }
    }
}