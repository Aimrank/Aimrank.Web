using Aimrank.Common.Domain;

namespace Aimrank.Domain.Lobbies.Events
{
    public class InvitationCreatedDomainEvent : IDomainEvent
    {
        public Lobby Lobby { get; }
        public LobbyInvitation Invitation { get; }

        public InvitationCreatedDomainEvent(Lobby lobby, LobbyInvitation invitation)
        {
            Lobby = lobby;
            Invitation = invitation;
        }
    }
}