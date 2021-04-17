using Aimrank.Web.Common.Domain;

namespace Aimrank.Web.Modules.Matches.Domain.Lobbies.Events
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