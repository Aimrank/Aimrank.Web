using System;

namespace Aimrank.Web.Hubs.Lobbies.Messages
{
    public class InvitationCanceledEventMessage
    {
        public Guid LobbyId { get; }
        public Guid InvitedUserId { get; }

        public InvitationCanceledEventMessage(Guid lobbyId, Guid invitedUserId)
        {
            LobbyId = lobbyId;
            InvitedUserId = invitedUserId;
        }
    }
}