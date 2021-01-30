using Aimrank.Application.Contracts;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Queries.GetMatchesHistory
{
    public class GetMatchesHistoryQueryHandler : IQueryHandler<GetMatchesHistoryQuery, IEnumerable<MatchHistoryDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMatchesHistoryQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<IEnumerable<MatchHistoryDto>> Handle(GetMatchesHistoryQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql =
                @"SELECT
                    [Match].[Id] AS [Id],
                    [Match].[ScoreT] AS [ScoreT],
                    [Match].[ScoreCT] AS [ScoreCT],
                    [Match].[CreatedAt] AS [CreatedAt],
                    (SELECT TOP (1) [Name] FROM [aimrank].[MatchesPlayers] AS [Player] WHERE [Player].[MatchId] = [Match].[Id] AND [Player].[Team] = 3) AS [TeamNameT],
                    (SELECT TOP (1) [Name] FROM [aimrank].[MatchesPlayers] AS [Player] WHERE [Player].[MatchId] = [Match].[Id] AND [Player].[Team] = 2) AS [TeamNameCT]
                  FROM [aimrank].[Matches] AS [Match]
                  ORDER BY [Match].[CreatedAt] DESC;";

            var matches = await connection.QueryAsync<MatchHistoryDto>(sql);

            return matches.AsList();
        }
    }
}