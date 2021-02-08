using System;

namespace Aimrank.Web.Hubs.Lobbies
{
    public class InvitationCanceledEvent
    {
        public Guid LobbyId { get; }
        public Guid InvitedUserId { get; }

        public InvitationCanceledEvent(Guid lobbyId, Guid invitedUserId)
        {
            LobbyId = lobbyId;
            InvitedUserId = invitedUserId;
        }
    }
}