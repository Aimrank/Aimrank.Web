using Aimrank.Application.Contracts;
using System;

namespace Aimrank.Application.Commands.Lobbies.AcceptLobbyInvitation
{
    public class AcceptLobbyInvitationCommand : ICommand
    {
        public Guid LobbyId { get; }

        public AcceptLobbyInvitationCommand(Guid lobbyId)
        {
            LobbyId = lobbyId;
        }
    }
}