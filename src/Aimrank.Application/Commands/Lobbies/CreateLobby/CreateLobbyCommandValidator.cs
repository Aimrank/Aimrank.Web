using FluentValidation;

namespace Aimrank.Application.Commands.Lobbies.CreateLobby
{
    internal class CreateLobbyCommandValidator : AbstractValidator<CreateLobbyCommand>
    {
        public CreateLobbyCommandValidator()
        {
            RuleFor(x => x.LobbyId).NotEmpty();
        }
    }
}