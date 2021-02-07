using Aimrank.Common.Domain;
using Aimrank.Domain.Users;
using System.Linq;

namespace Aimrank.Domain.Lobbies.Rules
{
    public class UserMustNotBeOnInvitationListRule : IBusinessRule
    {
        private readonly Lobby _lobby;
        private readonly User _user;

        public UserMustNotBeOnInvitationListRule(Lobby lobby, User user)
        {
            _lobby = lobby;
            _user = user;
        }

        public string Message => "User is already invited";
        public string Code => "user_already_invited";

        public bool IsBroken() => _lobby.Invitations.Any(i => i.InvitedUserId == _user.Id);
    }
}