using Aimrank.Common.Domain;
using Aimrank.Domain.Users;
using System.Linq;

namespace Aimrank.Domain.Lobbies.Rules
{
    public class InvitationMustExistForUserRule : IBusinessRule
    {
        private readonly Lobby _lobby;
        private readonly UserId _userId;

        public InvitationMustExistForUserRule(Lobby lobby, UserId userId)
        {
            _lobby = lobby;
            _userId = userId;
        }

        public string Message => "Invitation does not exist";
        public string Code => "invitation_not_found";

        public bool IsBroken() => _lobby.Invitations.All(i => i.InvitedUserId != _userId);
    }
}