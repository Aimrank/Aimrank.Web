using FluentValidation;

namespace Aimrank.Application.Commands.Lobbies.ChangeLobbyConfiguration
{
    internal class ChangeLobbyConfigurationCommandValidator : AbstractValidator<ChangeLobbyConfigurationCommand>
    {
        public ChangeLobbyConfigurationCommandValidator()
        {
            RuleFor(x => x.Map).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(450);
            RuleFor(x => x.Mode).InclusiveBetween(0, 1);
        }
    }
}