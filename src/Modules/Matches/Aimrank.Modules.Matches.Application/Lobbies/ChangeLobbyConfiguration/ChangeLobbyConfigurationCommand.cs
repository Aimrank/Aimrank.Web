using Aimrank.Modules.Matches.Application.Contracts;
using System;

namespace Aimrank.Modules.Matches.Application.Lobbies.ChangeLobbyConfiguration
{
    public class ChangeLobbyConfigurationCommand : ICommand
    {
        public Guid LobbyId { get; }
        public string Map { get; }
        public string Name { get; }
        public int Mode { get; }

        public ChangeLobbyConfigurationCommand(Guid lobbyId, string map, string name, int mode)
        {
            LobbyId = lobbyId;
            Map = map;
            Name = name;
            Mode = mode;
        }
    }
}