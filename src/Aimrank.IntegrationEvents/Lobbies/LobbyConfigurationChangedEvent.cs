using System;

namespace Aimrank.IntegrationEvents.Lobbies
{
    public class LobbyConfigurationChangedEvent : IntegrationEventBase
    {
        public Guid LobbyId { get; }
        public string Map { get; }
        public string Name { get; }
        public int Mode { get; }

        public LobbyConfigurationChangedEvent(Guid lobbyId, string map, string name, int mode)
        {
            LobbyId = lobbyId;
            Map = map;
            Name = name;
            Mode = mode;
        }
    }
}