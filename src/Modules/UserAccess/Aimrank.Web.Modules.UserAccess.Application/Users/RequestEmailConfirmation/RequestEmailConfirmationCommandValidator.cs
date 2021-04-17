using FluentValidation;

namespace Aimrank.Web.Modules.UserAccess.Application.Users.RequestEmailConfirmation
{
    public class RequestEmailConfirmationCommandValidator : AbstractValidator<RequestEmailConfirmationCommand>
    {
        public RequestEmailConfirmationCommandValidator()
        {
            RuleFor(x => x.UsernameOrEmail).NotEmpty().MaximumLength(255);
        }
    }
}