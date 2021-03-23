using Aimrank.Common.Domain;
using Aimrank.Modules.Matches.Domain.Matches;
using System.Collections.Generic;

namespace Aimrank.Modules.Matches.Domain.Lobbies
{
    public class LobbyConfiguration : ValueObject
    {
        private readonly string _maps;
        
        public string Name { get; }
        public MatchMode Mode { get; }

        public IEnumerable<string> Maps  => _maps.Split(',');
        
        private LobbyConfiguration()
        {
        }

        public LobbyConfiguration(string name, MatchMode mode, IEnumerable<string> maps)
        {
            Name = name;
            Mode = mode;
            _maps = string.Join(',', maps);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Mode;
            yield return _maps;
        }
    }
}