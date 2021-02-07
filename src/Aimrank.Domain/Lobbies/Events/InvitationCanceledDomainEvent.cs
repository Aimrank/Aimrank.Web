using Aimrank.Common.Domain;

namespace Aimrank.Domain.Lobbies.Events
{
    public class InvitationCanceledDomainEvent : IDomainEvent
    {
        public Lobby Lobby { get; }
        public LobbyInvitation Invitation { get; }

        public InvitationCanceledDomainEvent(Lobby lobby, LobbyInvitation invitation)
        {
            Lobby = lobby;
            Invitation = invitation;
        }
    }
}