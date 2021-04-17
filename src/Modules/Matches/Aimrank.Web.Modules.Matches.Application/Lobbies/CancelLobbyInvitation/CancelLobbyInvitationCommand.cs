using Aimrank.Web.Modules.Matches.Application.Contracts;
using System;

namespace Aimrank.Web.Modules.Matches.Application.Lobbies.CancelLobbyInvitation
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