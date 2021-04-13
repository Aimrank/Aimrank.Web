using Aimrank.Modules.CSGO.Application.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aimrank.Modules.CSGO.Application.Services
{
    public interface IPodClient
    {
        Task<IEnumerable<Pod>> GetInactivePodsAsync();
        Task StopServerAsync(Server server);
        Task StartServerAsync(Server server, string map, IEnumerable<string> whitelist);
    }
}