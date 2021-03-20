using Aimrank.Common.Domain;
using Aimrank.Modules.Matches.Domain.Players;
using System.Collections.Generic;
using System.Linq;

namespace Aimrank.Modules.Matches.Domain.Lobbies.Rules
{
    public class PlayerMustNotBeLobbyMemberRule : IBusinessRule
    {
        private readonly IEnumerable<LobbyMember> _members;
        private readonly PlayerId _playerId;

        public PlayerMustNotBeLobbyMemberRule(IEnumerable<LobbyMember> members, PlayerId playerId)
        {
            _members = members;
            _playerId = playerId;
        }

        public string Message => "This player is already a member of this lobby";
        public string Code => "player_already_this_lobby_member";
        
        public bool IsBroken() => _members.Any(m => m.PlayerId == _playerId);
    }
}