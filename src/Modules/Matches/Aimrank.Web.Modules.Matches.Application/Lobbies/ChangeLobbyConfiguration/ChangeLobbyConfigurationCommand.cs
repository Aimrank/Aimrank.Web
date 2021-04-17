using Aimrank.Web.Modules.Matches.Application.Contracts;
using System.Collections.Generic;
using System;

namespace Aimrank.Web.Modules.Matches.Application.Lobbies.ChangeLobbyConfiguration
{
    public class ChangeLobbyConfigurationCommand : ICommand
    {
        public Guid LobbyId { get; }
        public string Name { get; }
        public int Mode { get; }
        public IEnumerable<string> Maps { get; }

        public ChangeLobbyConfigurationCommand(Guid lobbyId, string name, int mode, IEnumerable<string> maps)
        {
            LobbyId = lobbyId;
            Name = name;
            Mode = mode;
            Maps = maps;
        }
    }
}