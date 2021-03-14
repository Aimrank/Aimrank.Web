using FluentValidation;

namespace Aimrank.Modules.Matches.Application.Lobbies.CancelLobbyInvitation
{
    internal class CancelLobbyInvitationCommandValidator : AbstractValidator<CancelLobbyInvitationCommand>
    {
        public CancelLobbyInvitationCommandValidator()
        {
            RuleFor(x => x.LobbyId).NotEmpty();
        }
    }
}