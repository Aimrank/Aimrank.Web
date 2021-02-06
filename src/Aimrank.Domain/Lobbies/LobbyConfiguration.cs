using Aimrank.Common.Domain;
using Aimrank.Domain.Lobbies.Rules;

namespace Aimrank.Domain.Lobbies
{
    public record LobbyConfiguration
    {
        public string Name { get; }
        public string Map { get; }
        public LobbyMatchMode Mode { get; }
        
        private LobbyConfiguration() {}

        public LobbyConfiguration(string name, string map, LobbyMatchMode mode)
        {
            BusinessRules.Check(new MapMustBeSupportedRule(map));
            
            Name = name;
            Map = map;
            Mode = mode;
        }
    }
}