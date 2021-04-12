using Aimrank.Modules.CSGO.Application.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aimrank.Modules.CSGO.Application.Repositories
{
    public interface IServerRepository
    {
        Task<IEnumerable<Server>> BrowseByIpAddressesAsync(IEnumerable<string> ipAddress);
        void DeleteRange(IEnumerable<Server> servers);
    }
}