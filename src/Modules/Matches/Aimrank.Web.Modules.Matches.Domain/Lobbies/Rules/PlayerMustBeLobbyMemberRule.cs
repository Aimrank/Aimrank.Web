using Aimrank.Web.Common.Domain;
using Aimrank.Web.Modules.Matches.Domain.Players;
using System.Collections.Generic;
using System.Linq;

namespace Aimrank.Web.Modules.Matches.Domain.Lobbies.Rules
{
    public class PlayerMustBeLobbyMemberRule : IBusinessRule
    {
        private readonly IEnumerable<LobbyMember> _members;
        private readonly PlayerId _playerId;

        public PlayerMustBeLobbyMemberRule(IEnumerable<LobbyMember> members, PlayerId playerId)
        {
            _members = members;
            _playerId = playerId;
        }

        public string Message => "You are not member of this lobby";
        public string Code => "player_not_lobby_member";
        
        public bool IsBroken() => _members.All(m => m.PlayerId != _playerId);
    }
}