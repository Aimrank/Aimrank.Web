using FluentValidation;

namespace Aimrank.Modules.Matches.Application.Lobbies.CreateLobby
{
    internal class CreateLobbyCommandValidator : AbstractValidator<CreateLobbyCommand>
    {
        public CreateLobbyCommandValidator()
        {
            RuleFor(x => x.LobbyId).NotEmpty();
        }
    }
}