using FluentValidation;

namespace Aimrank.Modules.Matches.Application.Lobbies.StartSearchingForGame
{
    internal class StartSearchingForGameCommandValidator : AbstractValidator<StartSearchingForGameCommand>
    {
        public StartSearchingForGameCommandValidator()
        {
            RuleFor(x => x.LobbyId).NotEmpty();
        }
    }
}