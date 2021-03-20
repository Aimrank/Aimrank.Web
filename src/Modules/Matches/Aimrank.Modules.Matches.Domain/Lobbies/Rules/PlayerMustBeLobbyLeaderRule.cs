using Aimrank.Common.Domain;
using Aimrank.Modules.Matches.Domain.Players;
using System.Collections.Generic;
using System.Linq;

namespace Aimrank.Modules.Matches.Domain.Lobbies.Rules
{
    public class PlayerMustBeLobbyLeaderRule : IBusinessRule
    {
        private readonly IEnumerable<LobbyMember> _members;
        private readonly PlayerId _playerId;

        public PlayerMustBeLobbyLeaderRule(IEnumerable<LobbyMember> members, PlayerId playerId)
        {
            _members = members;
            _playerId = playerId;
        }

        public string Message => "You are not a lobby leader";
        public string Code => "player_not_lobby_leader";

        public bool IsBroken() => !_members.Any(m => m.PlayerId == _playerId && m.Role == LobbyMemberRole.Leader);
    }
}