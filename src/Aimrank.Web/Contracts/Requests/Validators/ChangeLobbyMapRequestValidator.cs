using FluentValidation;

namespace Aimrank.Web.Contracts.Requests.Validators
{
    public class ChangeLobbyMapRequestValidator : AbstractValidator<ChangeLobbyMapRequest>
    {
        public ChangeLobbyMapRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        }
    }
}