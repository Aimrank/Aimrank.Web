using Aimrank.Common.Domain;
using Aimrank.Domain.Matches;
using System.Collections.Generic;

namespace Aimrank.Domain.Lobbies
{
    public class LobbyConfiguration : ValueObject
    {
        public string Name { get; }
        public string Map { get; }
        public MatchMode Mode { get; }

        public LobbyConfiguration(string name, string map, MatchMode mode)
        {
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