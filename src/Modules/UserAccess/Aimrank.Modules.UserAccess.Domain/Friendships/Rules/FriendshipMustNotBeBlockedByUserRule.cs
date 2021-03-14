using Aimrank.Common.Domain;
using Aimrank.Modules.UserAccess.Domain.Users;
using System.Collections.Generic;
using System.Linq;

namespace Aimrank.Modules.UserAccess.Domain.Friendships.Rules
{
    public class FriendshipMustNotBeBlockedByUserRule : IBusinessRule
    {
        private readonly IEnumerable<UserId> _blockingUsers;
        private readonly UserId _userId;

        public FriendshipMustNotBeBlockedByUserRule(IEnumerable<UserId> blockingUsers, UserId userId)
        {
            _blockingUsers = blockingUsers;
            _userId = userId;
        }

        public string Message => "Friendship is already blocked by specified user";
        public string Code => "friendship_already_blocked_by_user";

        public bool IsBroken() => _blockingUsers.Contains(_userId);
    }
}