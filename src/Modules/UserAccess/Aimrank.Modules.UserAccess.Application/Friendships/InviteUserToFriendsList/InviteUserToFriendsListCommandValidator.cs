using FluentValidation;

namespace Aimrank.Modules.UserAccess.Application.Friendships.InviteUserToFriendsList
{
    internal class InviteUserToFriendsListCommandValidator : AbstractValidator<InviteUserToFriendsListCommand>
    {
        public InviteUserToFriendsListCommandValidator()
        {
            RuleFor(x => x.InvitedUserId).NotEmpty();
        }
    }
}