using System.Collections.Generic;

namespace Aimrank.Infrastructure.Configuration.CSGO
{
    public class CSGOSettings
    {
        public bool UseFakeServerProcessManager { get; set; }
        public IEnumerable<string> SteamKeys { get; set; }
    }
}