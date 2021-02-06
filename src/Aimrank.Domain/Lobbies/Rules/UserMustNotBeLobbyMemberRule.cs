using Aimrank.Common.Domain;
using Aimrank.Domain.Users;
using System.Linq;

namespace Aimrank.Domain.Lobbies.Rules
{
    public class UserMustNotBeLobbyMemberRule : IBusinessRule
    {
        private readonly Lobby _lobby;
        private readonly User _user;

        public UserMustNotBeLobbyMemberRule(Lobby lobby, User user)
        {
            _lobby = lobby;
            _user = user;
        }

        public string Message => "User is already member of this lobby";
        public string Code => "user_already_this_lobby_member";
        
        public bool IsBroken() => _lobby.Members.Any(m => m.UserId == _user.Id);
    }
}