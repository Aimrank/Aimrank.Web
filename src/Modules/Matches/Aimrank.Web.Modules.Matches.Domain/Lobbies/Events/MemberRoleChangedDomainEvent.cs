using Aimrank.Web.Common.Domain;

namespace Aimrank.Web.Modules.Matches.Domain.Lobbies.Events
{
    public class MemberRoleChangedDomainEvent : DomainEvent
    {
        public LobbyId LobbyId { get; }
        public LobbyMember Member { get; }

        public MemberRoleChangedDomainEvent(LobbyId lobbyId, LobbyMember member)
        {
            LobbyId = lobbyId;
            Member = member;
        }
    }
}