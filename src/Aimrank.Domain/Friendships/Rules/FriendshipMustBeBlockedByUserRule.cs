using Aimrank.Common.Domain;
using Aimrank.Domain.Users;
using System.Collections.Generic;
using System.Linq;

namespace Aimrank.Domain.Friendships.Rules
{
    public class FriendshipMustBeBlockedByUserRule : IBusinessRule
    {
        private readonly IEnumerable<UserId> _blockingUsers;
        private readonly UserId _userId;

        public FriendshipMustBeBlockedByUserRule(IEnumerable<UserId> blockingUsers, UserId userId)
        {
            _blockingUsers = blockingUsers;
            _userId = userId;
        }

        public string Message => "Friendship is not blocked by specified user";
        public string Code => "friendship_not_blocked_by_user";

        public bool IsBroken() => !_blockingUsers.Contains(_userId);
    }
}