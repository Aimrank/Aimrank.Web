using Aimrank.Common.Application.Data;
using Aimrank.Modules.UserAccess.Application.Contracts;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Modules.UserAccess.Application.Users.GetUserStatsBatch
{
    public class GetUserStatsBatchQueryHandler : IQueryHandler<GetUserStatsBatchQuery, IEnumerable<UserStatsDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetUserStatsBatchQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<IEnumerable<UserStatsDto>> Handle(GetUserStatsBatchQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = @"
                SELECT
                    [P].[UserId] AS [UserId],
                    [M].[Mode] AS [Mode],
                    [M].[Map] AS [Map],
                    COUNT(*) AS [MatchesTotal],
                    SUM(CASE WHEN [M].[Winner] = [P].[Team] AND [M].[Winner] IN (2, 3) THEN 1 ELSE 0 END) AS [MatchesWon],
                    SUM([P].[Stats_Kills]) AS [TotalKills],
                    SUM([P].[Stats_Deaths]) AS [TotalDeaths],
                    SUM([P].[Stats_Hs]) AS [TotalHs]
                FROM [matches].[Matches] AS [M]
                INNER JOIN [matches].[MatchesPlayers] AS [P] ON [P].[MatchId] = [M].[Id]
                WHERE [P].[UserId] IN @UserIds
                GROUP BY [P].[UserId], [M].[Mode], [M].[Map];";

            var result = await connection.QueryAsync<UserStatsQueryResult>(sql, new {request.UserIds});

            var lookup = result.GroupBy(u => u.UserId).ToDictionary(g => g.Key);

            return request.UserIds.Select(id =>
            {
                var user = lookup.GetValueOrDefault(id);

                return user is null
                    ? null
                    : new UserStatsDto
                    {
                        MatchesTotal = user.Sum(r => r.MatchesTotal),
                        MatchesWon = user.Sum(r => r.MatchesWon),
                        TotalKills = user.Sum(r => r.TotalKills),
                        TotalDeaths = user.Sum(r => r.TotalDeaths),
                        TotalHs = user.Sum(r => r.TotalHs),
                        Modes = user.GroupBy(r => r.Mode).Select(m => new UserStatsModeDto
                        {
                            Mode = m.Key,
                            MatchesTotal = m.Sum(r => r.MatchesTotal),
                            MatchesWon = m.Sum(r => r.MatchesWon),
                            TotalKills = m.Sum(r => r.TotalKills),
                            TotalDeaths = m.Sum(r => r.TotalDeaths),
                            TotalHs = m.Sum(r => r.TotalHs),
                            Maps = m.GroupBy(r => r.Map).Select(m => new UserStatsMapDto
                            {
                                Map = m.Key,
                                MatchesTotal = m.Sum(r => r.MatchesTotal),
                                MatchesWon = m.Sum(r => r.MatchesWon),
                                TotalKills = m.Sum(r => r.TotalKills),
                                TotalDeaths = m.Sum(r => r.TotalDeaths),
                                TotalHs = m.Sum(r => r.TotalHs),
                            })
                        })
                    };
            });
        }
    }
}