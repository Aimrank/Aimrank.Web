using FluentValidation;

namespace Aimrank.Web.Contracts.Lobbies.Validators
{
    public class ChangeLobbyConfigurationRequestValidator : AbstractValidator<ChangeLobbyConfigurationRequest>
    {
        public ChangeLobbyConfigurationRequestValidator()
        {
            RuleFor(x => x.Map).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(450);
            RuleFor(x => x.Mode).InclusiveBetween(0, 1);
        }
    }
}