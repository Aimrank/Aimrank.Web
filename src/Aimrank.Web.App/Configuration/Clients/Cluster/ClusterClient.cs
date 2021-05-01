using Aimrank.Web.Modules.Matches.Application.Clients;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.App.Configuration.Clients.Cluster
{
    public class ClusterClient : IClusterClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _options = new()
        {
            PropertyNameCaseInsensitive = true
        };
        
        private HttpClient CreateClient() => _httpClientFactory.CreateClient(nameof(ClusterClient));

        public ClusterClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<GetAvailableServersCountResponse> GetAvailableServersCountAsync()
        {
            using var client = CreateClient();
            var response = await client.GetAsync("/server");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<GetAvailableServersCountResponse>(_options);
            }
            
            return  new GetAvailableServersCountResponse(0);
        }

        public async Task CreateServersAsync(CreateServersRequest request)
        {
            using var client = CreateClient();
            var response = await client.PostAsJsonAsync("/server", request);
            await ThrowIfErrorResponseAsync(response);
        }

        public async Task<string> StartServerAsync(StartServerRequest request)
        {
            using var client = CreateClient();
            var response = await client.PostAsJsonAsync("/server/start", request);
            await ThrowIfErrorResponseAsync(response);
            var content = await response.Content.ReadFromJsonAsync<StartServerResponse>();
            return content.Address;
        }

        public async Task DeleteServerAsync(Guid id)
        {
            using var client = CreateClient();
            var response = await client.DeleteAsync($"/server/{id}");
            await ThrowIfErrorResponseAsync(response);
        }

        public async Task AddSteamTokenAsync(AddSteamTokenRequest request)
        {
            using var client = CreateClient();
            var response = await client.PostAsJsonAsync("/steam-token", request);
            await ThrowIfErrorResponseAsync(response);
        }

        public async Task DeleteSteamTokenAsync(string token)
        {
            using var client = CreateClient();
            var response = await client.DeleteAsync($"/steam-token/{token}");
            await ThrowIfErrorResponseAsync(response);
        }

        public async Task<IEnumerable<SteamTokenDto>> GetSteamTokensAsync()
        {
            using var client = CreateClient();
            var response = await client.GetAsync("/steam-token");
            await ThrowIfErrorResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<IEnumerable<SteamTokenDto>>(_options);
        }

        private async Task ThrowIfErrorResponseAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode is false)
            {
                var content = await response.Content.ReadFromJsonAsync<ClusterApiError>(_options);
                throw new ClusterApiException(content);
            }
        }
    }
}