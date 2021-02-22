using Aimrank.Common.Application.Data;
using Aimrank.Domain.Matches;
using Aimrank.Domain.Users;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Aimrank.Infrastructure.Domain.Matches
{
    internal class MatchRepository : IMatchRepository
    {
        private readonly AimrankContext _context;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public MatchRepository(AimrankContext context, ISqlConnectionFactory sqlConnectionFactory)
        {
            _context = context;
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Dictionary<UserId, int>> BrowsePlayersRatingAsync(IEnumerable<UserId> ids, MatchMode mode)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = @"
                SELECT DISTINCT
                    [P].[UserId] AS [UserId],
                    FIRST_VALUE([P].[RatingEnd]) OVER (
                        PARTITION BY [P].[UserId]
                        ORDER BY [M].[FinishedAt] DESC
                    ) AS [Rating]
                FROM [aimrank].[Matches] AS [M]
                INNER JOIN [aimrank].[MatchesPlayers] AS [P] ON [P].[MatchId] = [M].[Id]
                WHERE
                    [M].[Mode] = @Mode AND
                    [M].[Status] = @Status AND
                    [P].[UserId] IN @Users;";

            var result = new Dictionary<UserId, int>();
            
            await connection.QueryAsync<Guid, int, int>(sql,
                (userId, rating) =>
                {
                    result[new UserId(userId)] = rating;
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