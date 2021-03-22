using Aimrank.Modules.Matches.Domain.Players;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aimrank.Modules.Matches.Domain.Matches
{
    public interface IMatchRepository
    {
        Task<Dictionary<PlayerId, int>> BrowsePlayersRatingAsync(IEnumerable<PlayerId> ids, MatchMode mode);
        Task<Match> GetByIdAsync(MatchId id);
        void Add(Match match);
        void Delete(Match match);
    }
}