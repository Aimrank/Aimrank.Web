using Aimrank.Domain.Matches;
using Aimrank.Domain.Users;
using Microsoft.EntityFrameworkCore;
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

        public Task<Match> GetByIdAsync(MatchId id)
            => _context.Matches.FirstOrDefaultAsync(m => m.Id == id);

        public async Task<int> GetPlayerRatingAsync(UserId id, MatchMode mode)
        {
            var match = await _context.Matches.AsNoTracking()
                .Where(m => m.Status == MatchStatus.Finished && m.Players.Any(p => p.UserId == id))
                .OrderByDescending(m => m.FinishedAt)
                .FirstOrDefaultAsync();

            return match?.Players.First(p => p.UserId == id).RatingEnd ?? 1200;
        }

        public void Add(Match match) => _context.Matches.Add(match);

        public void Update(Match match) => _context.Matches.Update(match);

        public void Delete(Match match) => _context.Matches.Remove(match);
    }
}