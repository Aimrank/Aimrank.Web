using FluentValidation;

namespace Aimrank.Web.Contracts.Lobbies.Validators
{
    public class InviteUserToLobbyRequestValidator : AbstractValidator<InviteUserToLobbyRequest>
    {
        public InviteUserToLobbyRequestValidator()
        {
            RuleFor(x => x.InvitedUserId).NotEmpty();
        }
    }
}