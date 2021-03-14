using FluentValidation;

namespace Aimrank.Modules.Matches.Application.Lobbies.AcceptLobbyInvitation
{
    internal class AcceptLobbyInvitationCommandValidator : AbstractValidator<AcceptLobbyInvitationCommand>
    {
        public AcceptLobbyInvitationCommandValidator()
        {
            RuleFor(x => x.LobbyId).NotEmpty();
        }
    }
}