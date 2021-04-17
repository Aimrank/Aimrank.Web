using Aimrank.Web.Modules.Cluster.Application.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aimrank.Web.Modules.Cluster.Application.Services
{
    public interface IPodClient
    {
        Task<IEnumerable<Pod>> GetInactivePodsAsync();
        Task StopServerAsync(Server server);
        Task<string> StartServerAsync(Server server, string map, IEnumerable<string> whitelist);
    }
}