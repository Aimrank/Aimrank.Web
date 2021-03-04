using FluentValidation;

namespace Aimrank.Web.Contracts.Friendships.Validators
{
    public class BlockUserRequestValidator : AbstractValidator<BlockUserRequest>
    {
        public BlockUserRequestValidator()
        {
            RuleFor(x => x.BlockedUserId).NotEmpty();
        }
    }
}