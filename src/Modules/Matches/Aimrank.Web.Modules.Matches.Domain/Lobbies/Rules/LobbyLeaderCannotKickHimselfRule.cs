using Aimrank.Web.Common.Domain;
using Aimrank.Web.Modules.Matches.Domain.Players;
using System.Collections.Generic;
using System.Linq;

namespace Aimrank.Web.Modules.Matches.Domain.Lobbies.Rules
{
    public class LobbyLeaderCannotKickHimselfRule : IBusinessRule
    {
        private readonly IEnumerable<LobbyMember> _members;
        private readonly PlayerId _kickingPlayerId;
        private readonly PlayerId _kickedPlayerId;

        public LobbyLeaderCannotKickHimselfRule(
            IEnumerable<LobbyMember> members,
            PlayerId kickingPlayerId,
            PlayerId kickedPlayerId)
        {
            _members = members;
            _kickingPlayerId = kickingPlayerId;
            _kickedPlayerId = kickedPlayerId;
        }

        public string Message => "You can't kick yourself";
        public string Code => "lobby_leader_cannot_kick_himself";

        public bool IsBroken() => _kickingPlayerId == _kickedPlayerId &&
                                  _members.Any(m => m.Role == LobbyMemberRole.Leader && m.PlayerId == _kickingPlayerId);
    }
}