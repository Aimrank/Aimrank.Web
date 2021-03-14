using Aimrank.Common.Domain;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Aimrank.Modules.Matches.Domain.Lobbies.Rules
{
    public class InvitationMustExistForUserRule : IBusinessRule
    {
        private readonly IEnumerable<LobbyInvitation> _invitations;
        private readonly Guid _userId;

        public InvitationMustExistForUserRule(IEnumerable<LobbyInvitation> invitations, Guid userId)
        {
            _invitations = invitations;
            _userId = userId;
        }

        public string Message => "Invitation does not exist";
        public string Code => "invitation_not_found";

        public bool IsBroken() => _invitations.All(i => i.InvitedUserId != _userId);
    }
}