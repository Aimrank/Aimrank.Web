using FluentValidation;

namespace Aimrank.Web.Contracts.Friendships.Validators
{
    public class AcceptFriendshipInvitationRequestValidator : AbstractValidator<AcceptFriendshipInvitationRequest>
    {
        public AcceptFriendshipInvitationRequestValidator()
        {
            RuleFor(x => x.InvitingUserId).NotEmpty();
        }
    }
}