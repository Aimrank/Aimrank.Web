using Aimrank.Common.Application.Exceptions;
using Aimrank.Modules.Matches.Domain.Players;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aimrank.Modules.Matches.Infrastructure.Domain.Players
{
    internal class PlayerRepository : IPlayerRepository
    {
        private readonly MatchesContext _context;

        public PlayerRepository(MatchesContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Player>> BrowseByIdAsync(IEnumerable<PlayerId> ids)
            => await _context.Players.AsNoTracking()
                .Where(u => ids.Contains(u.Id))
                .ToListAsync();
        
        public async Task<Player> GetByIdAsync(PlayerId id)
        {
            var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == id);
            if (player is null)
            {
                throw new EntityNotFoundException();
            }

            return player;
        }

        public Task<Player> GetByIdOptionalAsync(PlayerId id)
            => _context.Players.FirstOrDefaultAsync(p => p.Id == id);

        public Task<bool> ExistsSteamIdAsync(PlayerId id, string steamId)
            => _context.Players.AnyAsync(p => p.Id != id && p.SteamId == steamId);

        public void Add(Player player) => _context.Players.Add(player);
    }
}