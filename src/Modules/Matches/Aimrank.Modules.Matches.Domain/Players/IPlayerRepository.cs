using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aimrank.Modules.Matches.Domain.Players
{
    public interface IPlayerRepository
    {
        Task<IEnumerable<Player>> BrowseByIdAsync(IEnumerable<PlayerId> ids);
        Task<Player> GetByIdAsync(PlayerId id);
        Task<Player> GetByIdOptionalAsync(PlayerId id);
        Task<bool> ExistsSteamIdAsync(PlayerId id, string steamId);
        void Add(Player player);
        void Update(Player player);
    }
}