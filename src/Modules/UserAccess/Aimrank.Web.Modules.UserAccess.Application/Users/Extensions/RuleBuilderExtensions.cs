using FluentValidation;
using System.Linq.Expressions;
using System;

namespace Aimrank.Web.Modules.UserAccess.Application.Users.Extensions
{
    internal static class RuleBuilderExtensions
    {
        internal static IRuleBuilderOptions<T, string> Password<T>(this IRuleBuilder<T, string> builder)
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

        internal static IRuleBuilderOptions<T, string> PasswordMatch<T>(
            this IRuleBuilder<T, string> builder,
            Expression<Func<T, string>> passwordSelector)
        {
            return builder
                .NotEmpty()
                .Equal(passwordSelector)
                .WithMessage("Password does not match");
        }
    }
}