using Aimrank.Modules.CSGO.Application.Entities;
using Aimrank.Modules.CSGO.Application.Repositories;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Aimrank.Modules.CSGO.Application.Services
{
    public class PodClient : IPodClient
    {
        private readonly IPodRepository _podRepository;
        private readonly IHttpClientFactory _httpClientFactory;

        public PodClient(IPodRepository podRepository, IHttpClientFactory httpClientFactory)
        {
            _podRepository = podRepository;
            _httpClientFactory = httpClientFactory;
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

        public async Task StopServerAsync(Server server)
        {
            using var httpClient = _httpClientFactory.CreateClient();
            
            await httpClient.DeleteAsync($"http://{server.Pod.IpAddress}/server/{server.MatchId}");
        }
    }
}