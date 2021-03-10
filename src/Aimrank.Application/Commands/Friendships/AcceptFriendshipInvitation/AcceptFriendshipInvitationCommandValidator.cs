using FluentValidation;

namespace Aimrank.Application.Commands.Friendships.AcceptFriendshipInvitation
{
    internal class AcceptFriendshipInvitationCommandValidator : AbstractValidator<AcceptFriendshipInvitationCommand>
    {
        public AcceptFriendshipInvitationCommandValidator()
        {
            RuleFor(x => x.InvitingUserId).NotEmpty();
        }
    }
}