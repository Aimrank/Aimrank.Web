using System;

namespace Aimrank.IntegrationEvents.Lobbies
{
    public class InvitationCreatedEvent : IntegrationEventBase
    {
        public Guid LobbyId { get; }
        public Guid InvitedUserId { get; }

        public InvitationCreatedEvent(Guid lobbyId, Guid invitedUserId)
        {
            LobbyId = lobbyId;
            InvitedUserId = invitedUserId;
        }
    }
}