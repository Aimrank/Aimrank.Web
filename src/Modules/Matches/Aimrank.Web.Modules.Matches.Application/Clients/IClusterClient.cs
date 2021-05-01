using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.Modules.Matches.Application.Clients
{
    public record GetAvailableServersCountResponse(int Count);
    
    public record CreateServersRequest(IEnumerable<Guid> Ids);
    
    public record StartServerRequest(Guid Id, string Map, IEnumerable<string> Whitelist);

    public record StartServerResponse(string Address);
    
    public record AddSteamTokenRequest(string Token);
    
    public record DeleteSteamTokenRequest(string Token);
    
    public record SteamTokenDto(string Token, bool IsUsed);
    
    public interface IClusterClient
    {
        Task<GetAvailableServersCountResponse> GetAvailableServersCountAsync();
        Task CreateServersAsync(CreateServersRequest request);
        Task<string> StartServerAsync(StartServerRequest request);
        Task DeleteServerAsync(Guid id);
        Task AddSteamTokenAsync(AddSteamTokenRequest request);
        Task DeleteSteamTokenAsync(string token);
        Task<IEnumerable<SteamTokenDto>> GetSteamTokensAsync();
    }
}