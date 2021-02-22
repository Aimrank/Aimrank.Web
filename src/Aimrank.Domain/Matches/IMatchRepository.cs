using Aimrank.Domain.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aimrank.Domain.Matches
{
    public interface IMatchRepository
    {
        Task<Dictionary<UserId, int>> BrowsePlayersRatingAsync(IEnumerable<UserId> ids, MatchMode mode);
        Task<Match> GetByIdAsync(MatchId id);
        void Add(Match match);
        void Update(Match match);
        void Delete(Match match);
    }
}