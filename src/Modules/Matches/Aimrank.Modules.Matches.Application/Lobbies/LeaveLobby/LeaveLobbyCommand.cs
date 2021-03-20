using Aimrank.Modules.Matches.Application.Contracts;
using System;

namespace Aimrank.Modules.Matches.Application.Lobbies.LeaveLobby
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