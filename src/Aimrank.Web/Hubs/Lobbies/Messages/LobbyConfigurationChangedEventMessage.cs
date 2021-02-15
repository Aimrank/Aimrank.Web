using System;

namespace Aimrank.Web.Hubs.Lobbies.Messages
{
    public class LobbyConfigurationChangedEventMessage
    {
        public Guid LobbyId { get; }
        public string Map { get; }
        public string Name { get; }
        public int Mode { get; }

        public LobbyConfigurationChangedEventMessage(Guid lobbyId, string map, string name, int mode)
        {
            LobbyId = lobbyId;
            Map = map;
            Name = name;
            Mode = mode;
        }
    }
}