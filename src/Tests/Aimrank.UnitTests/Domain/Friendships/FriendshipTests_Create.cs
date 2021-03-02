using Aimrank.Domain.Friendships.Events;
using Aimrank.Domain.Friendships.Rules;
using Aimrank.Domain.Friendships;
using System.Threading.Tasks;
using Xunit;

namespace Aimrank.UnitTests.Domain.Friendships
{
    public class FriendshipTests_Create : FriendshipTestsBase
    {
        [Fact]
        public async Task CreateAsync_Emits_DomainEvent_When_Successfully_Created()
        {
            var friendship = await CreateFriendshipAsync(UserId1);
            
            friendship.ShouldEmitDomainEvent<FriendshipCreatedDomainEvent>();
        }
        
        [Fact]
        public async Task CreateAsync_Creates_Friendship_In_Pending_State_When_InvitingUser_Provided()
        {
            var friendship = await CreateFriendshipAsync(UserId1);
            
            Assert.Equal(FriendshipState.Pending, friendship.GetState());
        }

        [Fact]
        public async Task CreateAsync_Creates_Friendship_In_Blocked_State_When_BlockingUser_Provided()
        {
            var friendship = await CreateFriendshipAsync(null, UserId1);
            
            Assert.Equal(FriendshipState.Blocked, friendship.GetState());
        }
        
        [Fact]
        public async Task CreateAsync_Throws_If_Friendship_Is_Not_Unique()
        {
            var friendship1 = await CreateFriendshipAsync(UserId1);
            
            FriendshipRepository.Add(friendship1);

            async Task TestCode1() => await CreateFriendshipAsync(UserId1);
            async Task TestCode2() => await CreateFriendshipAsync(UserId2);
            
            await DomainAssert.FailsBusinessRuleAsync<FriendshipMustBeUniqueRule>(TestCode1);
            await DomainAssert.FailsBusinessRuleAsync<FriendshipMustBeUniqueRule>(TestCode2);
        }

        [Fact]
        public async Task CreateAsync_Throws_If_InvitingUser_And_BlockingUser_Are_Null()
        {
            async Task TestCode() => await CreateFriendshipAsync();

            await DomainAssert.FailsBusinessRuleAsync<FriendshipMustHaveInvitingUserOrBlockingUserRule>(TestCode);
        }

        [Fact]
        public async Task CreateAsync_Throws_If_InvitingUser_And_BlockingUser_Are_Both_Specified()
        {
            async Task TestCode() => await CreateFriendshipAsync(UserId1, UserId1);
            
            await DomainAssert.FailsBusinessRuleAsync<FriendshipMustHaveInvitingUserOrBlockingUserRule>(TestCode);
        }
    }
}