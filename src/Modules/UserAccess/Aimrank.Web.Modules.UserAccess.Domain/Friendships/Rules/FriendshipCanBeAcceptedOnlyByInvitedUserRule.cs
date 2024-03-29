using Aimrank.Web.Common.Domain;
using Aimrank.Web.Modules.UserAccess.Domain.Users;

namespace Aimrank.Web.Modules.UserAccess.Domain.Friendships.Rules
{
    public class FriendshipCanBeAcceptedOnlyByInvitedUserRule : IBusinessRule
    {
        private readonly UserId _invitedUserId;
        private readonly UserId _acceptingUserId;

        public FriendshipCanBeAcceptedOnlyByInvitedUserRule(UserId invitedUserId, UserId acceptingUserId)
        {
            _invitedUserId = invitedUserId;
            _acceptingUserId = acceptingUserId;
        }

        public string Message => "Friendship can be accepted only by invited user";
        public string Code => "friendship_accept_failed";

        public bool IsBroken() => _invitedUserId != _acceptingUserId;
    }
}