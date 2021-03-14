using FluentValidation;

namespace Aimrank.Modules.UserAccess.Application.Friendships.UnblockUser
{
    internal class UnblockUserCommandValidator : AbstractValidator<UnblockUserCommand>
    {
        public UnblockUserCommandValidator()
        {
            RuleFor(x => x.BlockedUserId).NotEmpty();
        }
    }
}