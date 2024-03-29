using Aimrank.Web.Common.Application.Exceptions;
using Aimrank.Web.Modules.Matches.Domain.Matches;
using Aimrank.Web.Modules.Matches.Domain.Players;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aimrank.Web.Modules.Matches.UnitTests.Mock
{
    internal class MatchRepositoryMock : IMatchRepository
    {
        private readonly Dictionary<MatchId, Match> _matches = new();

        public IEnumerable<Match> Matches => _matches.Values;

        public Task<IEnumerable<Match>> BrowseByIdAsync(IEnumerable<MatchId> ids)
            => Task.FromResult(_matches.Values.Where(m => ids.Contains(m.Id)));

        public Task<Dictionary<PlayerId, int>> BrowsePlayersRatingAsync(IEnumerable<PlayerId> ids, MatchMode mode)
        {
            var result = _matches.Values
                .Where(m => m.Mode == mode && m.Status == MatchStatus.Finished &&
                            m.Players.Any(p => ids.Contains(p.PlayerId)))
                .OrderByDescending(m => m.FinishedAt)
                .SelectMany(m => m.Players)
                .GroupBy(p => p.PlayerId)
                .Select(g => g.First())
                .ToDictionary(p => p.PlayerId, p => p.RatingEnd);

            return Task.FromResult(result);
        }

        public Task<Match> GetByIdAsync(MatchId id)
        {
            var match = _matches.GetValueOrDefault(id);
            if (match is null)
            {
                throw new EntityNotFoundException();
            }

            return Task.FromResult(match);
        }

        public void Add(Match match) => _matches.Add(match.Id, match);

        public void Delete(Match match) => _matches.Remove(match.Id);
    }
}