using Aimrank.Modules.Matches.Application.Contracts;
using System;

namespace Aimrank.Modules.Matches.Application.Lobbies.KickPlayerFromLobby
{
    public class KickPlayerFromLobbyCommand : ICommand
    {
        public Guid LobbyId { get; }
        public Guid PlayerId { get; }

        public KickPlayerFromLobbyCommand(Guid lobbyId, Guid playerId)
        {
            LobbyId = lobbyId;
            PlayerId = playerId;
        }
    }
}