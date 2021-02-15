using System;

namespace Aimrank.Web.Hubs.Lobbies.Messages
{
    public class InvitationCreatedEventMessage
    {
        public Guid LobbyId { get; }
        public Guid InvitingUserId { get; }
        public Guid InvitedUserId { get; }

        public InvitationCreatedEventMessage(Guid lobbyId, Guid invitingUserId, Guid invitedUserId)
        {
            LobbyId = lobbyId;
            InvitingUserId = invitingUserId;
            InvitedUserId = invitedUserId;
        }
    }
}