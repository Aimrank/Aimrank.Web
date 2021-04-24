using Aimrank.Web.Common.Application.Data;
using Aimrank.Web.Modules.Matches.Application.Contracts;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Application.Players.GetPlayerStatsBatch
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
                    p.player_id,
                    m.mode,
                    m.map,
                    COUNT(*) AS matches_total,
                    SUM(CASE WHEN m.winner = p.team AND m.winner IN (2, 3) THEN 1 ELSE 0 END) AS matches_won,
                    SUM(p.stats_kills) AS total_kills,
                    SUM(p.stats_deaths) AS total_deaths,
                    SUM(p.stats_hs) AS total_hs
                FROM matches.matches AS m
                INNER JOIN matches.matches_players AS p ON p.match_id = m.id
                WHERE p.player_id = ANY(@PlayerIds)
                GROUP BY p.player_id, m.mode, m.map;";

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