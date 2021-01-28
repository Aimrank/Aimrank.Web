using Aimrank.Domain.RefreshTokens;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aimrank.Infrastructure.Domain.RefreshTokens
{
    internal class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AimrankContext _context;

        public RefreshTokenRepository(AimrankContext context)
        {
            _context = context;
        }

        public Task<RefreshToken> GetAsync(RefreshTokenId id, string jwt)
            => _context.RefreshTokens.FirstOrDefaultAsync(t => t.Id == id && t.Jwt == jwt);

        public async Task<IEnumerable<RefreshToken>> GetByUserIdAsync(string userId)
            => await _context.RefreshTokens.Where(t => t.UserId == userId).ToListAsync();

        public void Add(RefreshToken refreshToken) => _context.RefreshTokens.Add(refreshToken);

        public void Update(RefreshToken refreshToken) => _context.RefreshTokens.Update(refreshToken);

        public void DeleteRange(IEnumerable<RefreshToken> refreshTokens) => _context.RefreshTokens.RemoveRange(refreshTokens);
    }
}