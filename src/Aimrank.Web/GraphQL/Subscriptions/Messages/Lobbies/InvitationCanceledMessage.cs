using System;

namespace Aimrank.Web.GraphQL.Subscriptions.Messages.Lobbies
{
    public class InvitationCanceledMessage
    {
        public Guid LobbyId { get; }
        public Guid InvitedPlayerId { get; }

        public InvitationCanceledMessage(Guid lobbyId, Guid invitedPlayerId)
        {
            LobbyId = lobbyId;
            InvitedPlayerId = invitedPlayerId;
        }
    }
}