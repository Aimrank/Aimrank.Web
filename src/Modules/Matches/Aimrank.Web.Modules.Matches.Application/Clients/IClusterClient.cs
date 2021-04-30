using Refit;
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
        [Get("/server")]
        Task<GetAvailableServersCountResponse> GetAvailableServersCountAsync();
        
        [Post("/server")]
        Task CreateServersAsync([Body] CreateServersRequest request);
        
        [Post("/server/start")]
        Task<StartServerResponse> StartServerAsync([Body] StartServerRequest request);
        
        [Delete("/server/{id}")]
        Task DeleteServerAsync(Guid id);
        
        [Post("/steam-token")]
        Task AddSteamTokenAsync([Body] AddSteamTokenRequest request);
        
        [Delete("/steam-token/{token}")]
        Task DeleteSteamTokenAsync(string token);
        
        [Get("/steam-token")]
        Task<IEnumerable<SteamTokenDto>> GetSteamTokensAsync();
    }
}