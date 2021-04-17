using Aimrank.Web.Modules.UserAccess.Application.Users.Extensions;
using FluentValidation;

namespace Aimrank.Web.Modules.UserAccess.Application.Users.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(x => x.OldPassword).NotEmpty();
            RuleFor(x => x.NewPassword).Password();
            RuleFor(x => x.RepeatNewPassword).PasswordMatch(x => x.NewPassword);
        }
    }
}