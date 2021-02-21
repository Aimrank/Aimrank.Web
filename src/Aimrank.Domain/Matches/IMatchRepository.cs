using Aimrank.Domain.Users;
using System.Threading.Tasks;

namespace Aimrank.Domain.Matches
{
    public interface IMatchRepository
    {
        Task<Match> GetByIdAsync(MatchId id);
        Task<int> GetPlayerRatingAsync(UserId id, MatchMode mode);
        void Add(Match match);
        void Update(Match match);
        void Delete(Match match);
    }
}