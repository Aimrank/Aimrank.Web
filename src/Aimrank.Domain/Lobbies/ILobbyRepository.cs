using Aimrank.Domain.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aimrank.Domain.Lobbies
{
    public interface ILobbyRepository
    {
        Task<IEnumerable<Lobby>> BrowseByStatusAsync(LobbyStatus? status);
        Task<IEnumerable<Lobby>> BrowseByIdAsync(IEnumerable<LobbyId> ids);
        Task<Lobby> GetByIdAsync(LobbyId id);
        Task<bool> ExistsForMemberAsync(UserId userId);
        void Add(Lobby lobby);
        void Update(Lobby lobby);
        void UpdateRange(IEnumerable<Lobby> lobbies);
        void Delete(Lobby lobby);
    }
}