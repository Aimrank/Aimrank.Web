using Aimrank.Common.Domain;
using Aimrank.Domain.Users;
using System.Collections.Generic;
using System.Linq;

namespace Aimrank.Domain.Lobbies.Rules
{
    public class UserMustNotBeOnInvitationListRule : IBusinessRule
    {
        private readonly IEnumerable<LobbyInvitation> _invitations;
        private readonly User _user;

        public UserMustNotBeOnInvitationListRule(IEnumerable<LobbyInvitation> invitations, User user)
        {
            _invitations = invitations;
            _user = user;
        }

        public string Message => "User is already invited";
        public string Code => "user_already_invited";

        public bool IsBroken() => _invitations.Any(i => i.InvitedUserId == _user.Id);
    }
}