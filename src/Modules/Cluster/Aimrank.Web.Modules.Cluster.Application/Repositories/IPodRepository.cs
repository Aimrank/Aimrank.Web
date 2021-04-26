using Aimrank.Web.Modules.Cluster.Application.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aimrank.Web.Modules.Cluster.Application.Repositories
{
    public interface IPodRepository
    {
        Task<Pod> GetByIpAddressOptionalAsync(string ipAddress);
        Task<IEnumerable<Pod>> BrowseAsync();
        void Add(Pod pod);
        void DeleteRange(IEnumerable<Pod> pods);
    }
}