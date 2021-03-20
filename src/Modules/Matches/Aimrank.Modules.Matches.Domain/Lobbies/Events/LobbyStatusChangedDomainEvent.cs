using Aimrank.Common.Domain;

namespace Aimrank.Modules.Matches.Domain.Lobbies.Events
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