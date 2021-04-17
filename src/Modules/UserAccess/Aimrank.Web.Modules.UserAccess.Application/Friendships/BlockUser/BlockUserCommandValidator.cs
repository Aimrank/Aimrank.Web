using FluentValidation;

namespace Aimrank.Web.Modules.UserAccess.Application.Friendships.BlockUser
{
    internal class BlockUserCommandValidator : AbstractValidator<BlockUserCommand>
    {
        public BlockUserCommandValidator()
        {
            RuleFor(x => x.BlockedUserId).NotEmpty();
        }
    }
}