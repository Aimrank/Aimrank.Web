using Aimrank.Common.Domain;
using Aimrank.Domain.Lobbies.Rules;
using Aimrank.Domain.Matches;

namespace Aimrank.Domain.Lobbies
{
    public record LobbyConfiguration
    {
        public string Name { get; }
        public string Map { get; }
        public MatchMode Mode { get; }
        
        private LobbyConfiguration() {}

        public LobbyConfiguration(string name, string map, MatchMode mode)
        {
            BusinessRules.Check(new MapMustBeSupportedRule(map));
            
            Name = name;
            Map = map;
            Mode = mode;
        }
    }
}