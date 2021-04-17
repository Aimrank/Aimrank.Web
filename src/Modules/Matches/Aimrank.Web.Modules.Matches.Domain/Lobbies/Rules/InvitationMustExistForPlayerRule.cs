using Aimrank.Web.Common.Domain;
using Aimrank.Web.Modules.Matches.Domain.Players;
using System.Collections.Generic;
using System.Linq;

namespace Aimrank.Web.Modules.Matches.Domain.Lobbies.Rules
{
    public class InvitationMustExistForPlayerRule : IBusinessRule
    {
        private readonly IEnumerable<LobbyInvitation> _invitations;
        private readonly PlayerId _playerId;

        public InvitationMustExistForPlayerRule(IEnumerable<LobbyInvitation> invitations, PlayerId playerId)
        {
            _invitations = invitations;
            _playerId = playerId;
        }

        public string Message => "Invitation does not exist";
        public string Code => "invitation_not_found";

        public bool IsBroken() => _invitations.All(i => i.InvitedPlayerId != _playerId);
    }
}