using FluentValidation;

namespace Aimrank.Web.Contracts.Friendships.Validators
{
    public class CreateFriendshipInvitationRequestValidator : AbstractValidator<CreateFriendshipInvitationRequest>
    {
        public CreateFriendshipInvitationRequestValidator()
        {
            RuleFor(x => x.InvitedUserId).NotEmpty();
        }
    }
}