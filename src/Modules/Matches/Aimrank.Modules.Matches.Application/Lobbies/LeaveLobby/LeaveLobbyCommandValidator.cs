using FluentValidation;

namespace Aimrank.Modules.Matches.Application.Lobbies.LeaveLobby
{
    internal class LeaveLobbyCommandValidator : AbstractValidator<LeaveLobbyCommand>
    {
        public LeaveLobbyCommandValidator()
        {
            RuleFor(x => x.LobbyId).NotEmpty();
        }
    }
}