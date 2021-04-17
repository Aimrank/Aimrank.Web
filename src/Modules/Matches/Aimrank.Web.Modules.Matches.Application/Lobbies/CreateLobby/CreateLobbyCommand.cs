using Aimrank.Web.Modules.Matches.Application.Contracts;
using System;

namespace Aimrank.Web.Modules.Matches.Application.Lobbies.CreateLobby
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