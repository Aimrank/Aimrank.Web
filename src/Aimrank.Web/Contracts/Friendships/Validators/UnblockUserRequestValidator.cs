using FluentValidation;

namespace Aimrank.Web.Contracts.Friendships.Validators
{
    public class UnblockUserRequestValidator : AbstractValidator<UnblockUserRequest>
    {
        public UnblockUserRequestValidator()
        {
            RuleFor(x => x.BlockedUserId).NotEmpty();
        }
    }
}