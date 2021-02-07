using Aimrank.Common.Domain;

namespace Aimrank.Domain.Lobbies.Events
{
    public class MemberLeftDomainEvent : IDomainEvent
    {
        public Lobby Lobby { get; }
        public LobbyMember Member { get; }

        public MemberLeftDomainEvent(Lobby lobby, LobbyMember member)
        {
            Lobby = lobby;
            Member = member;
        }
    }
}