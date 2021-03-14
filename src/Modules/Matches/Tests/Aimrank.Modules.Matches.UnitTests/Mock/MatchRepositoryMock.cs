using Aimrank.Common.Application.Exceptions;
using Aimrank.Modules.Matches.Domain.Matches;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Aimrank.Modules.Matches.UnitTests.Mock
{
    internal class MatchRepositoryMock : IMatchRepository
    {
        private readonly Dictionary<MatchId, Match> _matches = new();

        public IEnumerable<Match> Matches => _matches.Values;

        public Task<Dictionary<Guid, int>> BrowsePlayersRatingAsync(IEnumerable<Guid> ids, MatchMode mode)
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