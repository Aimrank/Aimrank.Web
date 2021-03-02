using Aimrank.Common.Domain;
using Aimrank.Domain.Users;

namespace Aimrank.Domain.Friendships.Rules
{
    public class UserMustBeFriendshipMemberRule : IBusinessRule
    {
        private readonly FriendshipMembers _members;
        private readonly UserId _userId;

        public UserMustBeFriendshipMemberRule(FriendshipMembers members, UserId userId)
        {
            _members = members;
            _userId = userId;
        }

        public string Message => "User is not member of this friendship";
        public string Code => "user_not_friendship_member";
        
        public bool IsBroken() => _members.UserId1 != _userId && _members.UserId2 != _userId;
    }
}