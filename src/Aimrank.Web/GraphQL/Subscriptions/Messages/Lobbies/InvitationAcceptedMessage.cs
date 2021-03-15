using System;

namespace Aimrank.Web.GraphQL.Subscriptions.Messages.Lobbies
{
    public class InvitationAcceptedMessage
    {
        public Guid LobbyId { get; }
        public Guid InvitedPlayerId { get; }

        public InvitationAcceptedMessage(Guid lobbyId, Guid invitedPlayerId)
        {
            LobbyId = lobbyId;
            InvitedPlayerId = invitedPlayerId;
        }
    }
}