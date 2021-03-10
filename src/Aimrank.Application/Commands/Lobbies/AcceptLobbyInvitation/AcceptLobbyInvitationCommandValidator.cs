using FluentValidation;

namespace Aimrank.Application.Commands.Lobbies.AcceptLobbyInvitation
{
    internal class AcceptLobbyInvitationCommandValidator : AbstractValidator<AcceptLobbyInvitationCommand>
    {
        public AcceptLobbyInvitationCommandValidator()
        {
            RuleFor(x => x.LobbyId).NotEmpty();
        }
    }
}