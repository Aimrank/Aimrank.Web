using Aimrank.Common.Domain;

namespace Aimrank.Domain.Lobbies.Events
{
    public class LobbyConfigurationChangedDomainEvent : IDomainEvent
    {
        public Lobby Lobby { get; }

        public LobbyConfigurationChangedDomainEvent(Lobby lobby)
        {
            Lobby = lobby;
        }
    }
}