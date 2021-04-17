using FluentValidation;

namespace Aimrank.Web.Modules.Matches.Application.Lobbies.InvitePlayerToLobby
{
    internal class InvitePlayerToLobbyCommandValidator : AbstractValidator<InvitePlayerToLobbyCommand>
    {
        public InvitePlayerToLobbyCommandValidator()
        {
            RuleFor(x => x.LobbyId).NotEmpty();
            RuleFor(x => x.InvitedPlayerId).NotEmpty();
        }
    }
}