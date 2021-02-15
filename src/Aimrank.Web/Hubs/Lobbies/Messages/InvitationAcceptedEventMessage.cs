using System;

namespace Aimrank.Web.Hubs.Lobbies.Messages
{
    public class InvitationAcceptedEventMessage
    {
        public Guid LobbyId { get; }
        public Guid InvitedUserId { get; }

        public InvitationAcceptedEventMessage(Guid lobbyId, Guid invitedUserId)
        {
            LobbyId = lobbyId;
            InvitedUserId = invitedUserId;
        }
    }
}