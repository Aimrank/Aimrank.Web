using System.Threading.Tasks;
using Aimrank.Domain.Users;

namespace Aimrank.Domain.Lobbies
{
    public interface ILobbyRepository
    {
        Task<Lobby> GetByIdAsync(LobbyId id);
        Task<bool> ExistsForMemberAsync(UserId userId);
        void Add(Lobby lobby);
        void Update(Lobby lobby);
        void Delete(Lobby lobby);
    }
}