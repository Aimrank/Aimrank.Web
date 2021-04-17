using FluentValidation;

namespace Aimrank.Web.Modules.UserAccess.Application.Friendships.DeleteFriendship
{
    internal class DeleteFriendshipCommandValidator : AbstractValidator<DeleteFriendshipCommand>
    {
        public DeleteFriendshipCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}