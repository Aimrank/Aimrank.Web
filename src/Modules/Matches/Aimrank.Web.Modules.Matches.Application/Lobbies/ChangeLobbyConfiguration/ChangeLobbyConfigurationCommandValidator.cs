using FluentValidation;

namespace Aimrank.Web.Modules.Matches.Application.Lobbies.ChangeLobbyConfiguration
{
    internal class ChangeLobbyConfigurationCommandValidator : AbstractValidator<ChangeLobbyConfigurationCommand>
    {
        public ChangeLobbyConfigurationCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(450);
            RuleFor(x => x.Mode).InclusiveBetween(0, 1);
            RuleFor(x => x.Maps).NotEmpty();
        }
    }
}