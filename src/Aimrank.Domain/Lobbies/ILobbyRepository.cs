using Aimrank.Domain.Matches;
using Aimrank.Domain.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aimrank.Domain.Lobbies
{
    public interface ILobbyRepository
    {
        Task<IEnumerable<Lobby>> BrowseAsync(LobbyStatus? status);
        Task<Lobby> GetByIdAsync(LobbyId id);
        Task<Lobby> GetByMatchIdAsync(MatchId id);
        Task<bool> ExistsForMemberAsync(UserId userId);
        void Add(Lobby lobby);
        void Update(Lobby lobby);
        void Delete(Lobby lobby);
    }
}