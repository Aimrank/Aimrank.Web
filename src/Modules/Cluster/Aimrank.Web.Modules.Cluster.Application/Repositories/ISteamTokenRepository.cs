using Aimrank.Web.Modules.Cluster.Application.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aimrank.Web.Modules.Cluster.Application.Repositories
{
    public interface ISteamTokenRepository
    {
        Task<IEnumerable<SteamToken>> BrowseUnusedAsync(int limit);
    }
}