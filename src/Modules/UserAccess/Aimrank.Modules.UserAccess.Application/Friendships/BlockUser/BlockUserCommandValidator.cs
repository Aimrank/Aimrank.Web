using FluentValidation;

namespace Aimrank.Modules.UserAccess.Application.Friendships.BlockUser
{
    internal class BlockUserCommandValidator : AbstractValidator<BlockUserCommand>
    {
        public BlockUserCommandValidator()
        {
            RuleFor(x => x.BlockedUserId).NotEmpty();
        }
    }
}