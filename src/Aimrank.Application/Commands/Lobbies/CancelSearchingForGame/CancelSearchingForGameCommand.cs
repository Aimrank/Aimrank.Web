using Aimrank.Application.Contracts;
using System;

namespace Aimrank.Application.Commands.Lobbies.CancelSearchingForGame
{
    public class CancelSearchingForGameCommand : ICommand
    {
        public Guid LobbyId { get; }

        public CancelSearchingForGameCommand(Guid lobbyId)
        {
            LobbyId = lobbyId;
        }
    }
}