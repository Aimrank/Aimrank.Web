using Aimrank.Common.Domain;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Aimrank.Modules.Matches.Domain.Lobbies.Rules
{
    public class UserMustBeLobbyLeaderRule : IBusinessRule
    {
        private readonly IEnumerable<LobbyMember> _members;
        private readonly Guid _userId;

        public UserMustBeLobbyLeaderRule(IEnumerable<LobbyMember> members, Guid userId)
        {
            _members = members;
            _userId = userId;
        }

        public string Message => "You are not a lobby leader";
        public string Code => "user_not_lobby_leader";

        public bool IsBroken() => !_members.Any(m => m.UserId == _userId && m.Role == LobbyMemberRole.Leader);
    }
}