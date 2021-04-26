using Aimrank.Web.Modules.Cluster.Application.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aimrank.Web.Modules.Cluster.Application.Repositories
{
    public interface ISteamTokenRepository
    {
        Task<SteamToken> GetAsync(string token);
        Task<SteamToken> GetOptionalAsync(string token);
        Task<IEnumerable<SteamToken>> BrowseUnusedAsync(int limit);
        void Add(SteamToken steamToken);
        void Delete(SteamToken steamToken);
    }
}