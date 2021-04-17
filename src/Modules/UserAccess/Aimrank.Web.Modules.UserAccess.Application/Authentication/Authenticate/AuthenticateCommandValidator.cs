using FluentValidation;

namespace Aimrank.Web.Modules.UserAccess.Application.Authentication.Authenticate
{
    internal class AuthenticateCommandValidator : AbstractValidator<AuthenticateCommand>
    {
        public AuthenticateCommandValidator()
        {
            RuleFor(x => x.UsernameOrEmail).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}