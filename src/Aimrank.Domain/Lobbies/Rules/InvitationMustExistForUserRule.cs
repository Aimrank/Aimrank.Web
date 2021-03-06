using Aimrank.Common.Domain;
using Aimrank.Domain.Users;
using System.Collections.Generic;
using System.Linq;

namespace Aimrank.Domain.Lobbies.Rules
{
    public class InvitationMustExistForUserRule : IBusinessRule
    {
        private readonly IEnumerable<LobbyInvitation> _invitations;
        private readonly UserId _userId;

        public InvitationMustExistForUserRule(IEnumerable<LobbyInvitation> invitations, UserId userId)
        {
            _invitations = invitations;
            _userId = userId;
        }

        public string Message => "Invitation does not exist";
        public string Code => "invitation_not_found";

        public bool IsBroken() => _invitations.All(i => i.InvitedUserId != _userId);
    }
}