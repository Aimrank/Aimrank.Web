using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.Modules.Matches.Application.Clients
{
    public record GetServerFleetResponse(
        int AllocatedReplicas,
        int ReadyReplicas,
        int Replicas,
        int ReservedReplicas);

    public record CreateReservationRequest(Guid Id, string Map, string Whitelist, DateTime ExpiresAt);
    
    public record CreateMatchRequest(Guid ReservationId);
    
    public record CreateMatchResponse(Guid MatchId, string Map, string Address, string Whitelist);

    public record AddSteamTokenRequest(string Token);
    
    public record DeleteSteamTokenRequest(string Token);
    
    public record SteamTokenDto(string Token, bool IsUsed);
    
    public interface IClusterClient
    {
        Task<GetServerFleetResponse> GetServerFleetAsync();
        Task CreateReservationAsync(CreateReservationRequest request);
        Task<string> CreateMatchAsync(CreateMatchRequest request);
        Task AddSteamTokenAsync(AddSteamTokenRequest request);
        Task DeleteSteamTokenAsync(string token);
        Task<IEnumerable<SteamTokenDto>> GetSteamTokensAsync();
    }
}