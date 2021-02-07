using System;

namespace Aimrank.IntegrationEvents.Lobbies
{
    public class InvitationCreatedEvent : IntegrationEventBase
    {
        public Guid LobbyId { get; }
        public Guid InvitingUserId { get; }
        public Guid InvitedUserId { get; }

        public InvitationCreatedEvent(Guid lobbyId, Guid invitingUserId, Guid invitedUserId)
        {
            LobbyId = lobbyId;
            InvitingUserId = invitingUserId;
            InvitedUserId = invitedUserId;
        }
    }
}