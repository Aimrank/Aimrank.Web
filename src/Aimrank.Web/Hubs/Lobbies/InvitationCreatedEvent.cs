using System;

namespace Aimrank.Web.Hubs.Lobbies
{
    public class InvitationCreatedEvent
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