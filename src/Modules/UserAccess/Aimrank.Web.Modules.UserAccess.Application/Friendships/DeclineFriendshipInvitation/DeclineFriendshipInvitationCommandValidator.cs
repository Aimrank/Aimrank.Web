using FluentValidation;

namespace Aimrank.Web.Modules.UserAccess.Application.Friendships.DeclineFriendshipInvitation
{
    internal class DeclineFriendshipInvitationCommandValidator : AbstractValidator<DeclineFriendshipInvitationCommand>
    {
        public DeclineFriendshipInvitationCommandValidator()
        {
            RuleFor(x => x.InvitingUserId).NotEmpty();
        }
    }
}