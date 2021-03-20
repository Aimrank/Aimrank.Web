using FluentValidation;

namespace Aimrank.Modules.UserAccess.Application.Users.RegisterNewUser
{
    internal static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, string> Password<T>(this IRuleBuilder<T, string> builder)
        {
            return builder
                .NotEmpty()
                .MinimumLength(6)
                .WithMessage("Password must contain at least 6 characters")
                .Matches("[$&+,:;=?@#|'<>.^*()%!-]")
                .WithMessage("Password must contain at least one special character")
                .Matches("[a-z]")
                .WithMessage("Password must contain at least one lowercase letter")
                .Matches("[A-Z]")
                .WithMessage("Password must contain at least one capital letter")
                .Matches("[0-9]")
                .WithMessage("Password must contain at least one numeric character");
        }
    }
    
    internal class RegisterNewUserCommandValidator : AbstractValidator<RegisterNewUserCommand>
    {
        public RegisterNewUserCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Username).NotEmpty().MaximumLength(32);
            RuleFor(x => x.Password).Password();
            RuleFor(x => x.PasswordRepeat)
                .NotEmpty()
                .Equal(x => x.Password)
                .WithMessage("Password does not match");
        }
    }
}