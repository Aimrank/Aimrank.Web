using FluentValidation;

namespace Aimrank.Application.Commands.Lobbies.LeaveLobby
{
    internal class LeaveLobbyCommandValidator : AbstractValidator<LeaveLobbyCommand>
    {
        public LeaveLobbyCommandValidator()
        {
            RuleFor(x => x.LobbyId).NotEmpty();
        }
    }
}