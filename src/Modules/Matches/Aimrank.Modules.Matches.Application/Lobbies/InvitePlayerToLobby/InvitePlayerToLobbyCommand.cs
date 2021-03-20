using Aimrank.Modules.Matches.Application.Contracts;
using System;

namespace Aimrank.Modules.Matches.Application.Lobbies.InvitePlayerToLobby
{
    public class InvitePlayerToLobbyCommand : ICommand
    {
        public Guid LobbyId { get; }
        public Guid InvitedPlayerId { get; }

        public InvitePlayerToLobbyCommand(Guid lobbyId, Guid invitedPlayerId)
        {
            LobbyId = lobbyId;
            InvitedPlayerId = invitedPlayerId;
        }
    }
}