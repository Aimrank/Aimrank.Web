using FluentValidation;

namespace Aimrank.Web.Modules.UserAccess.Application.Friendships.UnblockUser
{
    internal class UnblockUserCommandValidator : AbstractValidator<UnblockUserCommand>
    {
        public UnblockUserCommandValidator()
        {
            RuleFor(x => x.BlockedUserId).NotEmpty();
        }
    }
}