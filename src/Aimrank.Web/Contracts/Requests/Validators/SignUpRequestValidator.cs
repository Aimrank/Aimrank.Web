using FluentValidation;

namespace Aimrank.Web.Contracts.Requests.Validators
{
    public class SignUpRequestValidator : AbstractValidator<SignUpRequest>
    {
        public SignUpRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Username).NotEmpty().MaximumLength(32);
            RuleFor(x => x.Password).Password();
            RuleFor(x => x.RepeatPassword)
                .NotEmpty()
                .Equal(x => x.Password)
                .WithMessage("Password does not match");
        }
    }
}