using Aimrank.Web.Common.Application.Exceptions;
using Aimrank.Web.Modules.Matches.Domain.Players;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aimrank.Web.Modules.Matches.UnitTests.Mock
{
    internal class PlayerRepositoryMock : IPlayerRepository
    {
        private readonly Dictionary<PlayerId, Player> _players = new();

        public Task<IEnumerable<Player>> BrowseByIdAsync(IEnumerable<PlayerId> ids)
            => Task.FromResult(_players.Values.Where(p => ids.Contains(p.Id)));

        public Task<Player> GetByIdAsync(PlayerId id)
        {
            var player = _players.GetValueOrDefault(id);
            if (player is null)
            {
                throw new EntityNotFoundException();
            }

            return Task.FromResult(player);
        }

        public Task<Player> GetByIdOptionalAsync(PlayerId id)
            => Task.FromResult(_players.GetValueOrDefault(id));

        public Task<bool> ExistsSteamIdAsync(PlayerId id, string steamId)
            => Task.FromResult(_players.Values.Any(p => p.Id != id && p.SteamId == steamId));

        public void Add(Player player) => _players.Add(player.Id, player);
    }
}