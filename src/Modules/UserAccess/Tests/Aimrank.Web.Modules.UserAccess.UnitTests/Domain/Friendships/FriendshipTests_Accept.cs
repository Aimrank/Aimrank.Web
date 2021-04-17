using Aimrank.Web.Modules.UserAccess.Domain.Friendships.Rules;
using Aimrank.Web.Modules.UserAccess.Domain.Friendships;
using System.Threading.Tasks;
using Xunit;

namespace Aimrank.Web.Modules.UserAccess.UnitTests.Domain.Friendships
{
    public class FriendshipTests_Accept : FriendshipTestsBase
    {
        [Fact]
        public async Task Accept_Changes_Status_To_Active_When_Successfully_Accepted()
        {
            var friendship = await CreateFriendshipAsync(UserId1);
            
            friendship.Accept(UserId2);
            
            Assert.Equal(FriendshipState.Active, friendship.GetState());
        }
        
        [Fact]
        public async Task Accept_Throws_If_Friendship_Is_In_Accepted_State()
        {
            var friendship = await CreateFriendshipAsync(UserId1);
            
            void TestCode() => friendship.Accept(UserId2);
            
            DomainAssert.FailsBusinessRule<FriendshipMustHaveStateRule>(TestCode);
        }

        [Fact]
        public async Task Accept_Throws_If_Friendship_Is_In_Blocked_State()
        {
            var friendship = await CreateFriendshipAsync(null, UserId1);

            void TestCode() => friendship.Accept(UserId2);
            
            DomainAssert.FailsBusinessRule<FriendshipMustHaveStateRule>(TestCode);
        }

        [Fact]
        public async Task Accept_Throws_If_AcceptingUser_Is_Not_InvitedUser()
        {
            var friendship = await CreateFriendshipAsync(UserId1);

            void TestCode() => friendship.Accept(UserId1);
            
            DomainAssert.FailsBusinessRule<FriendshipCanBeAcceptedOnlyByInvitedUserRule>(TestCode);
        }
    }
}