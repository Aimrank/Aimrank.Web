using FluentValidation;

namespace Aimrank.Modules.UserAccess.Application.Users.RequestPasswordReminder
{
    public class RequestPasswordReminderCommandValidator : AbstractValidator<RequestPasswordReminderCommand>
    {
        public RequestPasswordReminderCommandValidator()
        {
            RuleFor(x => x.UsernameOrEmail).NotEmpty().MaximumLength(255);
        }
    }
}