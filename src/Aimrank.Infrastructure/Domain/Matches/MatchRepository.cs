using Aimrank.Domain.Matches;
using Microsoft.EntityFrameworkCore;
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

        public void Add(Match match) => _context.Matches.Add(match);

        public void Update(Match match) => _context.Matches.Update(match);

        public void Delete(Match match) => _context.Matches.Remove(match);
    }
}