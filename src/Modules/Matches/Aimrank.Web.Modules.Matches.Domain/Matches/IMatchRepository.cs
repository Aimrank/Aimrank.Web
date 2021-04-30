using Aimrank.Web.Modules.Matches.Domain.Players;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aimrank.Web.Modules.Matches.Domain.Matches
{
    public interface IMatchRepository
    {
        Task<IEnumerable<Match>> BrowseByIdAsync(IEnumerable<MatchId> ids);
        Task<Dictionary<PlayerId, int>> BrowsePlayersRatingAsync(IEnumerable<PlayerId> ids, MatchMode mode);
        Task<Match> GetByIdAsync(MatchId id);
        void Add(Match match);
        void Delete(Match match);
    }
}