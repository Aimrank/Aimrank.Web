using Aimrank.Web.Modules.UserAccess.Application.Users.Extensions;
using FluentValidation;

namespace Aimrank.Web.Modules.UserAccess.Application.Users.RegisterNewUser
{
    internal class RegisterNewUserCommandValidator : AbstractValidator<RegisterNewUserCommand>
    {
        public RegisterNewUserCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Username).NotEmpty().MaximumLength(32);
            RuleFor(x => x.Password).Password();
            RuleFor(x => x.PasswordRepeat).PasswordMatch(x => x.Password);
        }
    }
}