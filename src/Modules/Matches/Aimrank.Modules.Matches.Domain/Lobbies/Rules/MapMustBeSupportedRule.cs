using Aimrank.Common.Domain;
using System.Linq;

namespace Aimrank.Modules.Matches.Domain.Lobbies.Rules
{
    public class MapMustBeSupportedRule : IBusinessRule
    {
        private readonly string _name;

        public MapMustBeSupportedRule(string name)
        {
            _name = name;
        }

        public string Message => "This map is not supported";
        public string Code => "map_not_supported";

        public bool IsBroken() => !Maps.GetMaps().Contains(_name);
    }
}