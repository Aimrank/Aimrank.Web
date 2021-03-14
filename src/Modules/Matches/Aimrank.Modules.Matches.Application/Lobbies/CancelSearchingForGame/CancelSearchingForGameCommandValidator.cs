using FluentValidation;

namespace Aimrank.Modules.Matches.Application.Lobbies.CancelSearchingForGame
{
    internal class CancelSearchingForGameCommandValidator : AbstractValidator<CancelSearchingForGameCommand>
    {
        public CancelSearchingForGameCommandValidator()
        {
            RuleFor(x => x.LobbyId).NotEmpty();
        }
    }
}