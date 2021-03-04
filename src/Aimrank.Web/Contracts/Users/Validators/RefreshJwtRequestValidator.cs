using FluentValidation;

namespace Aimrank.Web.Contracts.Users.Validators
{
    public class RefreshJwtRequestValidator : AbstractValidator<RefreshJwtRequest>
    {
        public RefreshJwtRequestValidator()
        {
            RuleFor(x => x.Jwt).NotEmpty();
            RuleFor(x => x.RefreshToken).NotEmpty();
        }
    }
}