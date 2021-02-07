using Aimrank.Application.Contracts;
using System;

namespace Aimrank.Application.Commands.Lobbies.StartSearchingForGame
{
    public class StartSearchingForGameCommand : ICommand
    {
        public Guid LobbyId { get; }

        public StartSearchingForGameCommand(Guid lobbyId)
        {
            LobbyId = lobbyId;
        }
    }
}