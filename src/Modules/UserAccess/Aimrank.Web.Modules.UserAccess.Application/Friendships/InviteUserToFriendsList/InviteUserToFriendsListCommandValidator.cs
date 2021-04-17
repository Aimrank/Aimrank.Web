using FluentValidation;

namespace Aimrank.Web.Modules.UserAccess.Application.Friendships.InviteUserToFriendsList
{
    internal class InviteUserToFriendsListCommandValidator : AbstractValidator<InviteUserToFriendsListCommand>
    {
        public InviteUserToFriendsListCommandValidator()
        {
            RuleFor(x => x.InvitedUserId).NotEmpty();
        }
    }
}