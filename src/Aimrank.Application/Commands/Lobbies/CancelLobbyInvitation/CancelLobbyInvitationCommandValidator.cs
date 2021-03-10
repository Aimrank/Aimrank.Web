using FluentValidation;

namespace Aimrank.Application.Commands.Lobbies.CancelLobbyInvitation
{
    internal class CancelLobbyInvitationCommandValidator : AbstractValidator<CancelLobbyInvitationCommand>
    {
        public CancelLobbyInvitationCommandValidator()
        {
            RuleFor(x => x.LobbyId).NotEmpty();
        }
    }
}