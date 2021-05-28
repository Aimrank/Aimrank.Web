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
        private readonly JsonSerializerOptions _options = new() {PropertyNameCaseInsensitive = true};
        private readonly HttpClient _httpClient;

        public ClusterClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetServerFleetResponse> GetServerFleetAsync()
        {
            var response = await _httpClient.GetAsync("fleet");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<GetServerFleetResponse>(_options);
            }
            
            return new GetServerFleetResponse(0, 0, 0, 0);
        }

        public async Task CreateReservationAsync(CreateReservationRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("reservation", request);
            await ThrowIfErrorResponseAsync(response);
        }

        public async Task<string> CreateMatchAsync(CreateMatchRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("match", request);
            await ThrowIfErrorResponseAsync(response);
            var content = await response.Content.ReadFromJsonAsync<CreateMatchResponse>();
            return content.Address;
        }

        public async Task AddSteamTokenAsync(AddSteamTokenRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("steam-token", request);
            await ThrowIfErrorResponseAsync(response);
        }

        public async Task DeleteSteamTokenAsync(string token)
        {
            var response = await _httpClient.DeleteAsync($"steam-token/{token}");
            await ThrowIfErrorResponseAsync(response);
        }

        public async Task<IEnumerable<SteamTokenDto>> GetSteamTokensAsync()
        {
            var response = await _httpClient.GetAsync("steam-token");
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