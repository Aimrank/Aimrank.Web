using System;

namespace Aimrank.Web.Hubs.General.Messages
{
    public class LobbyInvitationCreatedEventMessage
    {
        public Guid LobbyId { get; }
        public Guid InvitingUserId { get; }
        public Guid InvitedUserId { get; }

        public LobbyInvitationCreatedEventMessage(Guid lobbyId, Guid invitingUserId, Guid invitedUserId)
        {
            LobbyId = lobbyId;
            InvitingUserId = invitingUserId;
            InvitedUserId = invitedUserId;
        }
    }
}