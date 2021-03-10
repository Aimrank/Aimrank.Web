using FluentValidation;

namespace Aimrank.Application.Commands.Friendships.BlockUser
{
    internal class BlockUserCommandValidator : AbstractValidator<BlockUserCommand>
    {
        public BlockUserCommandValidator()
        {
            RuleFor(x => x.BlockedUserId).NotEmpty();
        }
    }
}