using Aimrank.Application.Contracts;
using System;

namespace Aimrank.Application.Commands.Lobbies.LeaveLobby
{
    public class LeaveLobbyCommand : ICommand
    {
        public Guid LobbyId { get; }

        public LeaveLobbyCommand(Guid lobbyId)
        {
            LobbyId = lobbyId;
        }
    }
}