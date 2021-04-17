using Aimrank.Web.Modules.Cluster.Application.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.Modules.Cluster.Application.Repositories
{
    public interface IServerRepository
    {
        Task<IEnumerable<Server>> BrowseByIpAddressesAsync(IEnumerable<string> ipAddresses);
        Task<Server> GetByMatchIdAsync(Guid matchId);
        void Delete(Server server);
        void DeleteRange(IEnumerable<Server> servers);
    }
}