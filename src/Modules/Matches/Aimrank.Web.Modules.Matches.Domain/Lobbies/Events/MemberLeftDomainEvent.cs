using Aimrank.Web.Common.Domain;

namespace Aimrank.Web.Modules.Matches.Domain.Lobbies.Events
{
    public class MemberLeftDomainEvent : DomainEvent
    {
        public LobbyId LobbyId { get; }
        public LobbyMember Member { get; }

        public MemberLeftDomainEvent(LobbyId lobbyId, LobbyMember member)
        {
            LobbyId = lobbyId;
            Member = member;
        }
    }
}