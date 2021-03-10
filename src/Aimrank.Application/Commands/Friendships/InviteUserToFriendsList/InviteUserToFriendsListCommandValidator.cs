using FluentValidation;

namespace Aimrank.Application.Commands.Friendships.InviteUserToFriendsList
{
    internal class InviteUserToFriendsListCommandValidator : AbstractValidator<InviteUserToFriendsListCommand>
    {
        public InviteUserToFriendsListCommandValidator()
        {
            RuleFor(x => x.InvitedUserId).NotEmpty();
        }
    }
}