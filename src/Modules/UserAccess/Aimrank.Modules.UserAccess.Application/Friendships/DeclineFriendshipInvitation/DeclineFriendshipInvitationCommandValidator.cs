using FluentValidation;

namespace Aimrank.Modules.UserAccess.Application.Friendships.DeclineFriendshipInvitation
{
    internal class DeclineFriendshipInvitationCommandValidator : AbstractValidator<DeclineFriendshipInvitationCommand>
    {
        public DeclineFriendshipInvitationCommandValidator()
        {
            RuleFor(x => x.InvitingUserId).NotEmpty();
        }
    }
}