using System;

namespace Aimrank.Web.GraphQL.Subscriptions.Messages.Lobbies
{
    public class InvitationCanceledMessage
    {
        public Guid LobbyId { get; }
        public Guid InvitedUserId { get; }

        public InvitationCanceledMessage(Guid lobbyId, Guid invitedUserId)
        {
            LobbyId = lobbyId;
            InvitedUserId = invitedUserId;
        }
    }
}