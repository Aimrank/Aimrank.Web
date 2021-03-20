using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Aimrank.Modules.Matches.Domain.Lobbies
{
    public interface ILobbyRepository
    {
        Task<IEnumerable<Lobby>> BrowseByStatusAsync(LobbyStatus? status);
        Task<IEnumerable<Lobby>> BrowseByIdAsync(IEnumerable<LobbyId> ids);
        Task<Lobby> GetByIdAsync(LobbyId id);
        Task<bool> ExistsForMemberAsync(Guid userId);
        void Add(Lobby lobby);
        void Update(Lobby lobby);
        void UpdateRange(IEnumerable<Lobby> lobbies);
        void Delete(Lobby lobby);
    }
}