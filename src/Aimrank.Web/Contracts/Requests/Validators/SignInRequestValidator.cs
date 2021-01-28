using FluentValidation;

namespace Aimrank.Web.Contracts.Requests.Validators
{
    public class SignInRequestValidator : AbstractValidator<SignInRequest>
    {
        public SignInRequestValidator()
        {
            RuleFor(x => x.UsernameOrEmail).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}