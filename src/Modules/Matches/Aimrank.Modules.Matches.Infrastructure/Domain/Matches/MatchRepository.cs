using Aimrank.Common.Application.Data;
using Aimrank.Modules.Matches.Domain.Matches;
using Aimrank.Modules.Matches.Domain.Players;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aimrank.Modules.Matches.Infrastructure.Domain.Matches
{
    internal class MatchRepository : IMatchRepository
    {
        private readonly MatchesContext _context;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public MatchRepository(MatchesContext context, ISqlConnectionFactory sqlConnectionFactory)
        {
            _context = context;
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Dictionary<PlayerId, int>> BrowsePlayersRatingAsync(IEnumerable<PlayerId> ids, MatchMode mode)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = @"
                SELECT DISTINCT
                    [P].[UserId] AS [UserId],
                    FIRST_VALUE([P].[RatingEnd]) OVER (
                        PARTITION BY [P].[UserId]
                        ORDER BY [M].[FinishedAt] DESC
                    ) AS [Rating]
                FROM [matches].[Matches] AS [M]
                INNER JOIN [matches].[MatchesPlayers] AS [P] ON [P].[MatchId] = [M].[Id]
                WHERE
                    [M].[Mode] = @Mode AND
                    [M].[Status] = @Status AND
                    [P].[UserId] IN @Users;";

            var result = new Dictionary<PlayerId, int>();
            
            await connection.QueryAsync<PlayerId, int, int>(sql,
                (userId, rating) =>
                {
                    result[userId] = rating;
                    return rating;
                },
                new
                {
                    Mode = mode,
                    Status = MatchStatus.Finished,
                    Users = ids.Select(id => id.Value)
                },
                splitOn: "Rating");

            return result;
        }

        public Task<Match> GetByIdAsync(MatchId id)
            => _context.Matches.FirstOrDefaultAsync(m => m.Id == id);

        public void Add(Match match) => _context.Matches.Add(match);

        public void Update(Match match) => _context.Matches.Update(match);

        public void Delete(Match match) => _context.Matches.Remove(match);
    }
}