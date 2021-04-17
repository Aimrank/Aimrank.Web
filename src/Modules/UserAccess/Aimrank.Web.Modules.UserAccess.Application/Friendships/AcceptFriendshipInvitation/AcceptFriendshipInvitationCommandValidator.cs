using FluentValidation;

namespace Aimrank.Web.Modules.UserAccess.Application.Friendships.AcceptFriendshipInvitation
{
    internal class AcceptFriendshipInvitationCommandValidator : AbstractValidator<AcceptFriendshipInvitationCommand>
    {
        public AcceptFriendshipInvitationCommandValidator()
        {
            RuleFor(x => x.InvitingUserId).NotEmpty();
        }
    }
}