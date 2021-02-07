using System;

namespace Aimrank.IntegrationEvents.Lobbies
{
    public class InvitationAcceptedEvent : IntegrationEventBase
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