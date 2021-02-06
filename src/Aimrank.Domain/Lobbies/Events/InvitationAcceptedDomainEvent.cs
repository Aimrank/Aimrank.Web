using Aimrank.Common.Domain;

namespace Aimrank.Domain.Lobbies.Events
{
    public class InvitationAcceptedDomainEvent : IDomainEvent
    {
        public Lobby Lobby { get; }
        public LobbyInvitation Invitation { get; }

        public InvitationAcceptedDomainEvent(Lobby lobby, LobbyInvitation invitation)
        {
            Lobby = lobby;
            Invitation = invitation;
        }
    }
}