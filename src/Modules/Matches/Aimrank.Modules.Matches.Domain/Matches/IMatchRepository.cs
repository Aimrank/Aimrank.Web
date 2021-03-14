using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Aimrank.Modules.Matches.Domain.Matches
{
    public interface IMatchRepository
    {
        Task<Dictionary<Guid, int>> BrowsePlayersRatingAsync(IEnumerable<Guid> ids, MatchMode mode);
        Task<Match> GetByIdAsync(MatchId id);
        void Add(Match match);
        void Update(Match match);
        void Delete(Match match);
    }
}