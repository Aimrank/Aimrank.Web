using FluentValidation;

namespace Aimrank.Application.Commands.Friendships.UnblockUser
{
    internal class UnblockUserCommandValidator : AbstractValidator<UnblockUserCommand>
    {
        public UnblockUserCommandValidator()
        {
            RuleFor(x => x.BlockedUserId).NotEmpty();
        }
    }
}