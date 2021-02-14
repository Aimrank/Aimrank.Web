using System.Collections.Generic;

namespace Aimrank.Infrastructure.Application.CSGO
{
    internal record ServerConfiguration(string SteamKey, int Port, IEnumerable<string> Whitelist, string Map);
}