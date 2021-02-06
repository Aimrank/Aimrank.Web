using Aimrank.Common.Domain;

namespace Aimrank.Domain.Lobbies.Events
{
    public class MemberRoleChangedDomainEvent : IDomainEvent
    {
        public Lobby Lobby { get; }
        public LobbyMember Member { get; }

        public MemberRoleChangedDomainEvent(Lobby lobby, LobbyMember member)
        {
            Lobby = lobby;
            Member = member;
        }
    }
}