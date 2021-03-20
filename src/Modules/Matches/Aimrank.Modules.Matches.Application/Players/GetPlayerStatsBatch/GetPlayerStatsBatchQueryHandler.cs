using Aimrank.Common.Application.Data;
using Aimrank.Modules.Matches.Application.Contracts;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Modules.Matches.Application.Players.GetPlayerStatsBatch
{
    internal class GetPlayerStatsBatchQueryHandler : IQueryHandler<GetPlayerStatsBatchQuery, IEnumerable<PlayerStatsDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetPlayerStatsBatchQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<IEnumerable<PlayerStatsDto>> Handle(GetPlayerStatsBatchQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = @"
                SELECT
                    [P].[PlayerId] AS [PlayerId],
                    [M].[Mode] AS [Mode],
                    [M].[Map] AS [Map],
                    COUNT(*) AS [MatchesTotal],
                    SUM(CASE WHEN [M].[Winner] = [P].[Team] AND [M].[Winner] IN (2, 3) THEN 1 ELSE 0 END) AS [MatchesWon],
                    SUM([P].[Stats_Kills]) AS [TotalKills],
                    SUM([P].[Stats_Deaths]) AS [TotalDeaths],
                    SUM([P].[Stats_Hs]) AS [TotalHs]
                FROM [matches].[Matches] AS [M]
                INNER JOIN [matches].[MatchesPlayers] AS [P] ON [P].[MatchId] = [M].[Id]
                WHERE [P].[PlayerId] IN @PlayerIds
                GROUP BY [P].[PlayerId], [M].[Mode], [M].[Map];";

            var result = await connection.QueryAsync<PlayerStatsQueryResult>(sql, new {request.PlayerIds});

            var lookup = result.GroupBy(u => u.PlayerId).ToDictionary(g => g.Key);

            return request.PlayerIds.Select(id =>
            {
                var player = lookup.GetValueOrDefault(id);

                return player is null
                    ? null
                    : new PlayerStatsDto
                    {
                        MatchesTotal = player.Sum(r => r.MatchesTotal),
                        MatchesWon = player.Sum(r => r.MatchesWon),
                        TotalKills = player.Sum(r => r.TotalKills),
                        TotalDeaths = player.Sum(r => r.TotalDeaths),
                        TotalHs = player.Sum(r => r.TotalHs),
                        Modes = player.GroupBy(r => r.Mode).Select(m => new PlayerStatsModeDto
                        {
                            Mode = m.Key,
                            MatchesTotal = m.Sum(r => r.MatchesTotal),
                            MatchesWon = m.Sum(r => r.MatchesWon),
                            TotalKills = m.Sum(r => r.TotalKills),
                            TotalDeaths = m.Sum(r => r.TotalDeaths),
                            TotalHs = m.Sum(r => r.TotalHs),
                            Maps = m.GroupBy(r => r.Map).Select(m => new PlayerStatsMapDto
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