using FluentValidation;

namespace Aimrank.Web.Modules.Matches.Application.Lobbies.CancelSearchingForGame
{
    internal class CancelSearchingForGameCommandValidator : AbstractValidator<CancelSearchingForGameCommand>
    {
        public CancelSearchingForGameCommandValidator()
        {
            RuleFor(x => x.LobbyId).NotEmpty();
        }
    }
}