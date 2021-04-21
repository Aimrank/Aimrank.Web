using Aimrank.Web.Common.Domain;

namespace Aimrank.Web.Modules.Matches.Domain.Lobbies.Events
{
    public class LobbyStatusChangedDomainEvent : DomainEvent
    {
        public LobbyId LobbyId { get; }
        public LobbyStatus Status { get; }

        public LobbyStatusChangedDomainEvent(LobbyId lobbyId, LobbyStatus status)
        {
            LobbyId = lobbyId;
            Status = status;
        }
    }
}