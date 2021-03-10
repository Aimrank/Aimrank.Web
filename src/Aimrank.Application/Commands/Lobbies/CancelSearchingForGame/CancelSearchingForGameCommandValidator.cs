using FluentValidation;

namespace Aimrank.Application.Commands.Lobbies.CancelSearchingForGame
{
    internal class CancelSearchingForGameCommandValidator : AbstractValidator<CancelSearchingForGameCommand>
    {
        public CancelSearchingForGameCommandValidator()
        {
            RuleFor(x => x.LobbyId).NotEmpty();
        }
    }
}