using FluentValidation;

namespace Aimrank.Modules.UserAccess.Application.Friendships.AcceptFriendshipInvitation
{
    internal class AcceptFriendshipInvitationCommandValidator : AbstractValidator<AcceptFriendshipInvitationCommand>
    {
        public AcceptFriendshipInvitationCommandValidator()
        {
            RuleFor(x => x.InvitingUserId).NotEmpty();
        }
    }
}