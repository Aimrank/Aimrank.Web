using FluentValidation;

namespace Aimrank.Web.Modules.Matches.Application.Lobbies.CancelLobbyInvitation
{
    internal class CancelLobbyInvitationCommandValidator : AbstractValidator<CancelLobbyInvitationCommand>
    {
        public CancelLobbyInvitationCommandValidator()
        {
            RuleFor(x => x.LobbyId).NotEmpty();
        }
    }
}