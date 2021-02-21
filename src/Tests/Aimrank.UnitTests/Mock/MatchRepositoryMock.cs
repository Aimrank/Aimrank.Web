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

        public Task<Match> GetByIdAsync(MatchId id)
        {
            var match = _matches.GetValueOrDefault(id);
            if (match is null)
            {
                throw new EntityNotFoundException();
            }

            return Task.FromResult(match);
        }

        public Task<int> GetPlayerRatingAsync(UserId id, MatchMode mode)
        {
            var match = _matches.Values
                .Where(m => m.Status == MatchStatus.Finished && m.Players.Any(p => p.UserId == id))
                .OrderByDescending(m => m.FinishedAt)
                .FirstOrDefault();

            return Task.FromResult(match?.Players.First(p => p.UserId == id).RatingEnd ?? 1200);
        }

        public void Add(Match match) => _matches.Add(match.Id, match);

        public void Update(Match match) => _matches[match.Id] = match;

        public void Delete(Match match) => _matches.Remove(match.Id);
    }
}