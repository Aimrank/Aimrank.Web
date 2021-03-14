using System.Collections.Generic;

namespace Aimrank.Modules.Matches.Infrastructure.Application.CSGO
{
    internal record ServerConfiguration(string SteamKey, int Port, IEnumerable<string> Whitelist, string Map);
}