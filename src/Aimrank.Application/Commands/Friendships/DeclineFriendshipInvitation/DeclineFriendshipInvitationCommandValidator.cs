using FluentValidation;

namespace Aimrank.Application.Commands.Friendships.DeclineFriendshipInvitation
{
    internal class DeclineFriendshipInvitationCommandValidator : AbstractValidator<DeclineFriendshipInvitationCommand>
    {
        public DeclineFriendshipInvitationCommandValidator()
        {
            RuleFor(x => x.InvitingUserId).NotEmpty();
        }
    }
}