using Aimrank.Modules.UserAccess.Domain.Friendships;
using Aimrank.Modules.UserAccess.Domain.Users;
using Aimrank.Modules.UserAccess.UnitTests.Mock;
using System.Threading.Tasks;
using System;

namespace Aimrank.Modules.UserAccess.UnitTests.Domain.Friendships
{
    public class FriendshipTestsBase
    {
        protected readonly IFriendshipRepository FriendshipRepository = new FriendshipRepositoryMock();
        protected readonly UserId UserId1 = new(Guid.NewGuid());
        protected readonly UserId UserId2 = new(Guid.NewGuid());

        protected Task<Friendship> CreateFriendshipAsync(
            UserId invitingUserId = null,
            UserId blockingUserId = null)
            => Friendship.CreateAsync(
                new FriendshipMembers(UserId1, UserId2),
                FriendshipRepository,
                invitingUserId,
                blockingUserId);
    }
}