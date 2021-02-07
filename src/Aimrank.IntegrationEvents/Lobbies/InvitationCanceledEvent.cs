using System;

namespace Aimrank.IntegrationEvents.Lobbies
{
    public class InvitationCanceledEvent : IntegrationEventBase
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