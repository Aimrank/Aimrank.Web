using Aimrank.Common.Domain;
using Aimrank.Domain.Users;
using System.Linq;

namespace Aimrank.Domain.Lobbies.Rules
{
    public class UserMustBeLobbyMemberRule : IBusinessRule
    {
        private readonly Lobby _lobby;
        private readonly User _user;

        public UserMustBeLobbyMemberRule(Lobby lobby, User user)
        {
            _lobby = lobby;
            _user = user;
        }

        public string Message => "You are not member of this lobby";
        public string Code => "user_not_lobby_member";
        
        public bool IsBroken() => _lobby.Members.All(m => m.UserId != _user.Id);
    }
}