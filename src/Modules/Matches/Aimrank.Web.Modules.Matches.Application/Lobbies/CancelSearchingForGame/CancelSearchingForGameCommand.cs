using Aimrank.Web.Modules.Matches.Application.Contracts;
using System;

namespace Aimrank.Web.Modules.Matches.Application.Lobbies.CancelSearchingForGame
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