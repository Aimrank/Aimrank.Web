using Aimrank.Web.Common.Domain;

namespace Aimrank.Web.Modules.Matches.Domain.Lobbies.Events
{
    public class LobbyStatusChangedDomainEvent : IDomainEvent
    {
        public Lobby Lobby { get; }

        public LobbyStatusChangedDomainEvent(Lobby lobby)
        {
            Lobby = lobby;
        }
    }
}