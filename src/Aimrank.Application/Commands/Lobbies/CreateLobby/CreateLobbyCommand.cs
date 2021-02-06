using Aimrank.Application.Contracts;
using System;

namespace Aimrank.Application.Commands.Lobbies.CreateLobby
{
    public class CreateLobbyCommand : ICommand
    {
        public Guid LobbyId { get; }

        public CreateLobbyCommand(Guid lobbyId)
        {
            LobbyId = lobbyId;
        }
    }
}