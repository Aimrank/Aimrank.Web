using System.Collections.Generic;

namespace Aimrank.Web.Modules.Matches.Domain.Lobbies
{
    public static class Maps
    {
        public const string AimMap = "aim_map";
        public const string AimRedline = "am_redline_14";
            
        private static readonly HashSet<string> MapNames = new()
        {
            AimMap,
            AimRedline
        };

        public static IEnumerable<string> GetMaps() => MapNames;
    }
}