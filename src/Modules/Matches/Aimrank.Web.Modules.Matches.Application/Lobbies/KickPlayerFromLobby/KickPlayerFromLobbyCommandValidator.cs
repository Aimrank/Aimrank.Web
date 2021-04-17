using FluentValidation;

namespace Aimrank.Web.Modules.Matches.Application.Lobbies.KickPlayerFromLobby
{
    public class KickPlayerFromLobbyCommandValidator : AbstractValidator<KickPlayerFromLobbyCommand>
    {
        public KickPlayerFromLobbyCommandValidator()
        {
            RuleFor(x => x.LobbyId).NotEmpty();
            RuleFor(x => x.PlayerId).NotEmpty();
        }
    }
}