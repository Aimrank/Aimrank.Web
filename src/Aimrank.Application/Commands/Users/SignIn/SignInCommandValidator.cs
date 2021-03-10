using FluentValidation;

namespace Aimrank.Application.Commands.Users.SignIn
{
    internal class SignInCommandValidator : AbstractValidator<SignInCommand>
    {
        public SignInCommandValidator()
        {
            RuleFor(x => x.UsernameOrEmail).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}