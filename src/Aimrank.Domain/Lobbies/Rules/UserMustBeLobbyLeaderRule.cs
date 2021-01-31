using Aimrank.Common.Domain;
using Aimrank.Domain.Users;
using System.Linq;

namespace Aimrank.Domain.Lobbies.Rules
{
    public class UserMustBeLobbyLeaderRule : IBusinessRule
    {
        private readonly Lobby _lobby;
        private readonly UserId _userId;

        public UserMustBeLobbyLeaderRule(Lobby lobby, UserId userId)
        {
            _lobby = lobby;
            _userId = userId;
        }

        public string Message => "You are not a lobby leader";
        public string Code => "not_lobby_leader";

        public bool IsBroken() => !_lobby.Members.Any(m => m.UserId == _userId && m.Role == LobbyMemberRole.Leader);
    }
}