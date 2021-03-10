using System;

namespace Aimrank.Web.GraphQL.Subscriptions.Messages.Lobbies
{
    public class InvitationAcceptedMessage
    {
        public Guid LobbyId { get; }
        public Guid InvitedUserId { get; }

        public InvitationAcceptedMessage(Guid lobbyId, Guid invitedUserId)
        {
            LobbyId = lobbyId;
            InvitedUserId = invitedUserId;
        }
    }
}