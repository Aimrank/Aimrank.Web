using Aimrank.Common.Domain;
using Aimrank.Domain.Users;

namespace Aimrank.Domain.Friendships.Rules
{
    public class FriendshipCanBeDeclinedOnlyByInvitedUserRule : IBusinessRule
    {
        private readonly UserId _invitedUserId;
        private readonly UserId _decliningUserId;

        public FriendshipCanBeDeclinedOnlyByInvitedUserRule(UserId invitedUserId, UserId decliningUserId)
        {
            _invitedUserId = invitedUserId;
            _decliningUserId = decliningUserId;
        }

        public string Message => "Only invited user can decline friendship invitation";
        public string Code => "friendship_decline_failed";

        public bool IsBroken() => _invitedUserId != _decliningUserId;
    }
}