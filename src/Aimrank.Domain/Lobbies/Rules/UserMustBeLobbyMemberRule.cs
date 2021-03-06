using Aimrank.Common.Domain;
using Aimrank.Domain.Users;
using System.Collections.Generic;
using System.Linq;

namespace Aimrank.Domain.Lobbies.Rules
{
    public class UserMustBeLobbyMemberRule : IBusinessRule
    {
        private readonly IEnumerable<LobbyMember> _members;
        private readonly User _user;

        public UserMustBeLobbyMemberRule(IEnumerable<LobbyMember> members, User user)
        {
            _members = members;
            _user = user;
        }

        public string Message => "You are not member of this lobby";
        public string Code => "user_not_lobby_member";
        
        public bool IsBroken() => _members.All(m => m.UserId != _user.Id);
    }
}