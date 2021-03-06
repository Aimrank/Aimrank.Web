using Aimrank.Common.Domain;
using Aimrank.Domain.Users;
using System.Collections.Generic;
using System.Linq;

namespace Aimrank.Domain.Lobbies.Rules
{
    public class UserMustNotBeLobbyMemberRule : IBusinessRule
    {
        private readonly IEnumerable<LobbyMember> _members;
        private readonly User _user;

        public UserMustNotBeLobbyMemberRule(IEnumerable<LobbyMember> members, User user)
        {
            _members = members;
            _user = user;
        }

        public string Message => "User is already member of this lobby";
        public string Code => "user_already_this_lobby_member";
        
        public bool IsBroken() => _members.Any(m => m.UserId == _user.Id);
    }
}