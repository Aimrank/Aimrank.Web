using Aimrank.Common.Domain;
using Aimrank.Domain.Lobbies.Rules;
using Aimrank.Domain.Matches;
using System.Collections.Generic;

namespace Aimrank.Domain.Lobbies
{
    public class LobbyConfiguration : ValueObject
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

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Map;
            yield return Mode;
        }
    }
}