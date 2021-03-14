using Aimrank.Common.Domain;
using Aimrank.Modules.UserAccess.Domain.Users;

namespace Aimrank.Modules.UserAccess.Domain.Friendships.Rules
{
    public class FriendshipMustHaveInvitingUserOrBlockingUserRule : IBusinessRule
    {
        private readonly UserId _invitingUserId;
        private readonly UserId _blockingUserId;

        public FriendshipMustHaveInvitingUserOrBlockingUserRule(UserId invitingUserId, UserId blockingUserId)
        {
            _invitingUserId = invitingUserId;
            _blockingUserId = blockingUserId;
        }

        public string Message => "Either inviting user or blocking user must be specified";
        public string Code => "invalid_friendship_members";

        public bool IsBroken() =>
            (_invitingUserId is null && _blockingUserId is null) ||
            (_invitingUserId is not null && _blockingUserId is not null);
    }
}