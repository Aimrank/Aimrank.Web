using FluentValidation;

namespace Aimrank.Web.Contracts.Friendships.Validators
{
    public class DeclineFriendshipInvitationRequestValidator : AbstractValidator<DeclineFriendshipInvitationRequest>
    {
        public DeclineFriendshipInvitationRequestValidator()
        {
            RuleFor(x => x.InvitingUserId).NotEmpty();
        }
    }
}