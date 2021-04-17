using Aimrank.Web.Modules.UserAccess.Application.Users.Extensions;
using FluentValidation;

namespace Aimrank.Web.Modules.UserAccess.Application.Users.ResetPassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Token).NotEmpty();
            RuleFor(x => x.NewPassword).Password();
            RuleFor(x => x.RepeatNewPassword).PasswordMatch(x => x.NewPassword);
        }
    }
}