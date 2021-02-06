using FluentValidation;

namespace Aimrank.Web.Contracts.Requests.Validators
{
    public class InviteUserToLobbyRequestValidator : AbstractValidator<InviteUserToLobbyRequest>
    {
        public InviteUserToLobbyRequestValidator()
        {
            RuleFor(x => x.InvitedUserId).NotEmpty();
        }
    }
}