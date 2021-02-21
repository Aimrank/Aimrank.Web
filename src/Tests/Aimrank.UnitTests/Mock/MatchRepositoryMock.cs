using Aimrank.Common.Application.Exceptions;
using Aimrank.Domain.Matches;
using Aimrank.Domain.Users;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aimrank.UnitTests.Mock
{
    internal class MatchRepositoryMock : IMatchRepository
    {
        private readonly Dictionary<MatchId, Match> _matches = new();

        public IEnumerable<Match> Matches => _matches.Values;

        public Task<Dictionary<UserId, int>> BrowsePlayersRatingAsync(IEnumerable<UserId> ids, MatchMode mode)
        {
            var result = _matches.Values
                .Where(m => m.Mode == mode && m.Status == MatchStatus.Finished &&
                            m.Players.Any(p => ids.Contains(p.UserId)))
                .OrderByDescending(m => m.FinishedAt)
                .SelectMany(m => m.Players)
                .GroupBy(p => p.UserId)
                .Select(g => g.First())
                .ToDictionary(p => p.UserId, p => p.RatingEnd);

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

        public void Update(Match match) => _matches[match.Id] = match;

        public void Delete(Match match) => _matches.Remove(match.Id);
    }
}