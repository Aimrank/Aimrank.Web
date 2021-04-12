using Aimrank.Modules.CSGO.Application.Entities;
using Aimrank.Modules.CSGO.Application.Repositories;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Aimrank.Modules.CSGO.Application.Services
{
    public class PodService : IPodService
    {
        private readonly IPodRepository _podRepository;

        public PodService(IPodRepository podRepository)
        {
            _podRepository = podRepository;
        }

        public async Task<IEnumerable<Pod>> GetInactivePodsAsync()
        {
            var inactivePods = new List<Pod>();
            
            var pods = await _podRepository.BrowseAsync();

            var sender = new Ping();

            foreach (var pod in pods)
            {
                var result = sender.Send(pod.IpAddress);

                if (result.Status != IPStatus.Success)
                {
                    inactivePods.Add(pod);
                }
            }

            return inactivePods;
        }
    }
}