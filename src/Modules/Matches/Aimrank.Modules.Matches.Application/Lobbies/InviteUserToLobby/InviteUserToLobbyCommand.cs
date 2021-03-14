using Aimrank.Modules.Matches.Application.Contracts;
using System;

namespace Aimrank.Modules.Matches.Application.Lobbies.InviteUserToLobby
{
    public class InviteUserToLobbyCommand : ICommand
    {
        public Guid LobbyId { get; }
        public Guid InvitedUserId { get; }

        public InviteUserToLobbyCommand(Guid lobbyId, Guid invitedUserId)
        {
            LobbyId = lobbyId;
            InvitedUserId = invitedUserId;
        }
    }
}