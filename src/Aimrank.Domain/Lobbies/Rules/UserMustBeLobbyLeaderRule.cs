using Aimrank.Common.Domain;
using Aimrank.Domain.Users;
using System.Collections.Generic;
using System.Linq;

namespace Aimrank.Domain.Lobbies.Rules
{
    public class UserMustBeLobbyLeaderRule : IBusinessRule
    {
        private readonly IEnumerable<LobbyMember> _members;
        private readonly UserId _userId;

        public UserMustBeLobbyLeaderRule(IEnumerable<LobbyMember> members, UserId userId)
        {
            _members = members;
            _userId = userId;
        }

        public string Message => "You are not a lobby leader";
        public string Code => "user_not_lobby_leader";

        public bool IsBroken() => !_members.Any(m => m.UserId == _userId && m.Role == LobbyMemberRole.Leader);
    }
}