using FluentValidation;

namespace Aimrank.Application.Commands.Friendships.DeleteFriendship
{
    internal class DeleteFriendshipCommandValidator : AbstractValidator<DeleteFriendshipCommand>
    {
        public DeleteFriendshipCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}