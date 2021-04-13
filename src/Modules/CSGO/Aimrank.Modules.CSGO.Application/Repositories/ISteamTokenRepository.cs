using Aimrank.Modules.CSGO.Application.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aimrank.Modules.CSGO.Application.Repositories
{
    public interface ISteamTokenRepository
    {
        Task<IEnumerable<SteamToken>> BrowseUnusedAsync(int limit);
    }
}