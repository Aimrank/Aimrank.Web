using Aimrank.Modules.CSGO.Application.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aimrank.Modules.CSGO.Application.Services
{
    public interface IPodService
    {
        Task<IEnumerable<Pod>> GetInactivePodsAsync();
    }
}