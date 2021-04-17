using Aimrank.Web.Modules.Matches.Application.Contracts;
using System;

namespace Aimrank.Web.Modules.Matches.Application.Lobbies.StartSearchingForGame
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