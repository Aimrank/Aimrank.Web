using FluentValidation;

namespace Aimrank.Application.Commands.Lobbies.StartSearchingForGame
{
    internal class StartSearchingForGameCommandValidator : AbstractValidator<StartSearchingForGameCommand>
    {
        public StartSearchingForGameCommandValidator()
        {
            RuleFor(x => x.LobbyId).NotEmpty();
        }
    }
}