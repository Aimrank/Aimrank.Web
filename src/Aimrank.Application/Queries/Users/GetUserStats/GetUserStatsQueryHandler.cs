using Aimrank.Application.Contracts;
using Aimrank.Common.Application.Data;
using Dapper;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Queries.Users.GetUserStats
{
    public class GetUserStatsQueryHandler : IQueryHandler<GetUserStatsQuery, UserStatsDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetUserStatsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<UserStatsDto> Handle(GetUserStatsQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = @"
                SELECT
                    [M].[Mode] AS [Mode],
                    [M].[Map] AS [Map],
                    COUNT(*) AS [MatchesTotal],
                    SUM(CASE WHEN [M].[Winner] = [P].[Team] AND [M].[Winner] IN (2, 3) THEN 1 ELSE 0 END) AS [MatchesWon],
                    SUM([P].[Stats_Kills]) AS [TotalKills],
                    SUM([P].[Stats_Deaths]) AS [TotalDeaths],
                    SUM([P].[Stats_Hs]) AS [TotalHs]
                FROM [aimrank].[Matches] AS [M]
                INNER JOIN [aimrank].[MatchesPlayers] AS [P] ON [P].[MatchId] = [M].[Id]
                WHERE [P].[UserId] = @UserId
                GROUP BY [M].[Mode], [M].[Map];";

            
            var result = await connection.QueryAsync<UserStatsQueryResult>(sql, new {request.UserId});

            return new UserStatsDto
            {
                MatchesTotal = result.Sum(r => r.MatchesTotal),
                MatchesWon = result.Sum(r => r.MatchesWon),
                TotalKills = result.Sum(r => r.TotalKills),
                TotalDeaths = result.Sum(r => r.TotalDeaths),
                TotalHs = result.Sum(r => r.TotalHs),
                Modes = result.GroupBy(r => r.Mode).Select(m => new UserStatsModeDto
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
        }
    }
}