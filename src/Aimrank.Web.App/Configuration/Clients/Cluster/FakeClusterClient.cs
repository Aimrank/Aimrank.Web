using Aimrank.Web.Modules.Matches.Application.Clients;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aimrank.Web.App.Configuration.Clients.Cluster
{
    public class FakeClusterClient : IClusterClient
    {
        private readonly Dictionary<string, SteamTokenDto> _steamTokens = new();
        
        public Task<GetServerFleetResponse> GetServerFleetAsync() =>
            Task.FromResult(new GetServerFleetResponse(0, 10, 10, 0));

        public Task CreateReservationAsync(CreateReservationRequest request) => Task.CompletedTask;

        public Task<string> CreateMatchAsync(CreateMatchRequest request) => Task.FromResult("localhost");

        public Task AddSteamTokenAsync(AddSteamTokenRequest request)
        {
            _steamTokens.Add(request.Token, new SteamTokenDto(request.Token, false));
            return Task.CompletedTask;
        }

        public Task DeleteSteamTokenAsync(string token)
        {
            _steamTokens.Remove(token);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<SteamTokenDto>> GetSteamTokensAsync() =>
            Task.FromResult(_steamTokens.Values.AsEnumerable());
    }
}