using FluentValidation;

namespace Aimrank.Web.Contracts.Requests.Validators
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, string> Password<T>(this IRuleBuilder<T, string> builder)
        {
            return builder
                .NotEmpty()
                .MinimumLength(8)
                .WithMessage("Password must contain at least 8 characters")
                .Matches("[$&+,:;=?@#|'<>.^*()%!-]")
                .WithMessage("Password must contain at least one special character")
                .Matches("[A-Z]")
                .WithMessage("Password must contain at least one capital letter")
                .Matches("[0-9]")
                .WithMessage("Password must contain at least one numeric character");
        }
    }
}