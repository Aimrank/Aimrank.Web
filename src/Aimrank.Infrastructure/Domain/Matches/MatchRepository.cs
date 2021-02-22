using Aimrank.Domain.Matches;
using Aimrank.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aimrank.Infrastructure.Domain.Matches
{
    internal class MatchRepository : IMatchRepository
    {
        private readonly AimrankContext _context;

        public MatchRepository(AimrankContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<UserId, int>> BrowsePlayersRatingAsync(IEnumerable<UserId> ids, MatchMode mode)
            => await _context.Matches.AsNoTracking()
                .Where(m => m.Mode == mode && m.Status == MatchStatus.Finished &&
                            m.Players.Any(p => ids.Contains(p.UserId)))
                .OrderByDescending(m => m.FinishedAt)
                .SelectMany(m => m.Players)
                .ToDictionaryAsync(p => p.UserId, p => p.RatingEnd);

        public Task<Match> GetByIdAsync(MatchId id)
            => _context.Matches.FirstOrDefaultAsync(m => m.Id == id);

        public void Add(Match match) => _context.Matches.Add(match);

        public void Update(Match match) => _context.Matches.Update(match);

        public void Delete(Match match) => _context.Matches.Remove(match);
    }
}