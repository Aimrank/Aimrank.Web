using System;

namespace Aimrank.Web.GraphQL.Subscriptions.Messages.Lobbies
{
    public class LobbyConfigurationChangedMessage
    {
        public Guid LobbyId { get; }
        public string Map { get; }
        public string Name { get; }
        public int Mode { get; }

        public LobbyConfigurationChangedMessage(Guid lobbyId, string map, string name, int mode)
        {
            LobbyId = lobbyId;
            Map = map;
            Name = name;
            Mode = mode;
        }
    }
}