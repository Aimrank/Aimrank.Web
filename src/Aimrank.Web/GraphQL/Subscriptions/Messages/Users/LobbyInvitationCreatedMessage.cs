using System;

namespace Aimrank.Web.GraphQL.Subscriptions.Messages.Users
{
    public class LobbyInvitationCreatedMessage
    {
        public Guid LobbyId { get; }
        public Guid InvitingPlayerId { get; }
        public Guid InvitedPlayerId { get; }

        public LobbyInvitationCreatedMessage(Guid lobbyId, Guid invitingPlayerId, Guid invitedPlayerId)
        {
            LobbyId = lobbyId;
            InvitingPlayerId = invitingPlayerId;
            InvitedPlayerId = invitedPlayerId;
        }
    }
}