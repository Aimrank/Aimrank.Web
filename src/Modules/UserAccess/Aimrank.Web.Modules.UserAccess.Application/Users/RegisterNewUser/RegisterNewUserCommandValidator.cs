using Aimrank.Web.Modules.UserAccess.Application.Users.Extensions;
using FluentValidation;

namespace Aimrank.Web.Modules.UserAccess.Application.Users.RegisterNewUser
{
    internal class RegisterNewUserCommandValidator : AbstractValidator<RegisterNewUserCommand>
    {
        public RegisterNewUserCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(320);
            RuleFor(x => x.Username).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Password).Password();
            RuleFor(x => x.PasswordRepeat).PasswordMatch(x => x.Password);
        }
    }
}