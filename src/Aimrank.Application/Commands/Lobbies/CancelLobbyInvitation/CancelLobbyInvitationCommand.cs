using Aimrank.Application.Contracts;
using System;

namespace Aimrank.Application.Commands.Lobbies.CancelLobbyInvitation
{
    public class CancelLobbyInvitationCommand : ICommand
    {
        public Guid LobbyId { get; }

        public CancelLobbyInvitationCommand(Guid lobbyId)
        {
            LobbyId = lobbyId;
        }
    }
}