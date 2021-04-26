using Aimrank.Web.Modules.Cluster.Application.Entities;
using Aimrank.Web.Modules.Cluster.Application.Repositories;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Aimrank.Web.Modules.Cluster.Application.Clients
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
            using var httpClient = _httpClientFactory.CreateClient();
            
            var inactivePods = new List<Pod>();
            
            var pods = await _podRepository.BrowseAsync();

            foreach (var pod in pods)
            {
                if (await IsPodAliveAsync(httpClient, pod) is false)
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

        public async Task<string> StartServerAsync(Server server, string map, IEnumerable<string> whitelist)
        {
            using var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.PostAsJsonAsync($"http://{server.Pod.IpAddress}/server", new
            {
                server.MatchId,
                SteamToken = server.SteamToken.Token,
                Map = map,
                Whitelist = whitelist
            });

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ServerDto>();
                return result?.Address;
            }

            return null;
        }

        private async Task<bool> IsPodAliveAsync(HttpClient httpClient, Pod pod)
        {
            try
            {
                var result = await httpClient.GetAsync($"http://{pod.IpAddress}");
                return result.IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }

        private record ServerDto(string Address);
    }
}