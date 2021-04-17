using Aimrank.Web.Common.Domain;
using Aimrank.Web.Modules.Matches.Domain.Players;
using System.Collections.Generic;
using System.Linq;

namespace Aimrank.Web.Modules.Matches.Domain.Lobbies.Rules
{
    public class PlayerMustNotBeOnInvitationListRule : IBusinessRule
    {
        private readonly IEnumerable<LobbyInvitation> _invitations;
        private readonly PlayerId _playerId;

        public PlayerMustNotBeOnInvitationListRule(IEnumerable<LobbyInvitation> invitations, PlayerId playerId)
        {
            _invitations = invitations;
            _playerId = playerId;
        }

        public string Message => "Invitation already sent";
        public string Code => "lobby_invitation_already_sent";

        public bool IsBroken() => _invitations.Any(i => i.InvitedPlayerId == _playerId);
    }
}