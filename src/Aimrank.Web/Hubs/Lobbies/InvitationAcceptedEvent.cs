using System;

namespace Aimrank.Web.Hubs.Lobbies
{
    public class InvitationAcceptedEvent
    {
        public Guid LobbyId { get; }
        public Guid InvitedUserId { get; }

        public InvitationAcceptedEvent(Guid lobbyId, Guid invitedUserId)
        {
            LobbyId = lobbyId;
            InvitedUserId = invitedUserId;
        }
    }
}