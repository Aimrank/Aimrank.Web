using Aimrank.Common.Domain;

namespace Aimrank.Modules.Matches.Domain.Lobbies.Events
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