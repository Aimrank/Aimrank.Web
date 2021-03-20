using FluentValidation;

namespace Aimrank.Modules.UserAccess.Application.Friendships.DeleteFriendship
{
    internal class DeleteFriendshipCommandValidator : AbstractValidator<DeleteFriendshipCommand>
    {
        public DeleteFriendshipCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}