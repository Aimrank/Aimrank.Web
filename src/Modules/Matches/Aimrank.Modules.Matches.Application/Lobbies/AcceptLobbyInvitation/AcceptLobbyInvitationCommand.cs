using Aimrank.Modules.Matches.Application.Contracts;
using System;

namespace Aimrank.Modules.Matches.Application.Lobbies.AcceptLobbyInvitation
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