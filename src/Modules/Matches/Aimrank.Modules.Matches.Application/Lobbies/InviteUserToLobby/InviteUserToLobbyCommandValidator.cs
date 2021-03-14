using FluentValidation;

namespace Aimrank.Modules.Matches.Application.Lobbies.InviteUserToLobby
{
    internal class InviteUserToLobbyCommandValidator : AbstractValidator<InviteUserToLobbyCommand>
    {
        public InviteUserToLobbyCommandValidator()
        {
            RuleFor(x => x.LobbyId).NotEmpty();
            RuleFor(x => x.InvitedUserId).NotEmpty();
        }
    }
}