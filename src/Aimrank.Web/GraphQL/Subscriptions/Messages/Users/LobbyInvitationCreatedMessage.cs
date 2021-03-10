using System;

namespace Aimrank.Web.GraphQL.Subscriptions.Messages.Users
{
    public class LobbyInvitationCreatedMessage
    {
        public Guid LobbyId { get; }
        public Guid InvitingUserId { get; }
        public Guid InvitedUserId { get; }

        public LobbyInvitationCreatedMessage(Guid lobbyId, Guid invitingUserId, Guid invitedUserId)
        {
            LobbyId = lobbyId;
            InvitingUserId = invitingUserId;
            InvitedUserId = invitedUserId;
        }
    }
}