using Aimrank.Modules.CSGO.Application.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aimrank.Modules.CSGO.Application.Repositories
{
    public interface IPodRepository
    {
        Task<Pod> GetByIpAddressAsync(string ipAddress);
        Task<IEnumerable<Pod>> BrowseAsync();
        void Add(Pod pod);
        void DeleteRange(IEnumerable<Pod> pods);
    }
}