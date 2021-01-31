using Aimrank.Domain.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aimrank.Domain.RefreshTokens
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetAsync(RefreshTokenId id, string jwt);
        Task<IEnumerable<RefreshToken>> GetByUserIdAsync(UserId userId);
        void Add(RefreshToken refreshToken);
        void Update(RefreshToken refreshToken);
        void DeleteRange(IEnumerable<RefreshToken> refreshTokens);
    }
}