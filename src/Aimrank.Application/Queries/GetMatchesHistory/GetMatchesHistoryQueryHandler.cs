using Aimrank.Application.Contracts;
using Aimrank.Common.Application.Data;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

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
            
            const string sqlWhereUser = @"
				WHERE [Match].[Id] IN (
					SELECT TOP 30 [M].[Id]
					FROM [aimrank].[Matches] AS [M]
					INNER JOIN [aimrank].[MatchesPlayers] AS [P] on [M].[Id] = [P].[MatchId]
					WHERE
						[P].[UserId] = @UserId AND
						[M].[Status] = 4
					ORDER BY [M].[FinishedAt] DESC
				)";

            const string sqlWhereStatus = "WHERE [Match].[Status] = 4";

            var conditions = request.UserId.HasValue ? sqlWhereUser : sqlWhereStatus;
            
            var sql = @$"
				WITH [Result] AS (
					SELECT
						DENSE_RANK() OVER(ORDER BY [Match].[FinishedAt] DESC) AS [Row],
						[Match].[Id] AS [Id],
						[Match].[ScoreT] AS [ScoreT],
						[Match].[ScoreCT] AS [ScoreCT],
						[Match].[CreatedAt] AS [CreatedAt],
						[Match].[FinishedAt] AS [FinishedAt],
						[Match].[Map] AS [Map],
						[User].[Id] AS [User_Id],
						[User].[UserName] AS [User_Username],
						[Player].[Kills] AS [User_Kills],
						[Player].[Assists] AS [User_Assists],
						[Player].[Deaths] AS [User_Deaths],
						[Player].[Score] AS [User_Score]
					FROM [aimrank].[Matches] AS [Match]
					INNER JOIN [aimrank].[MatchesPlayers] AS [Player] ON [Match].[Id] = [Player].[MatchId]
					INNER JOIN [aimrank].[AspNetUsers] AS [User] ON [Player].[UserId] = [User].[Id]
					{conditions}
				)
				SELECT * FROM [Result] WHERE [Row] <= 30;";

            var lookup = new Dictionary<Guid, MatchHistoryDto>();

            await connection.QueryAsync<MatchHistoryDto, MatchHistoryPlayerQueryResult, MatchHistoryDto>(
	            sql,
	            (match, player) =>
	            {
		            if (!lookup.TryGetValue(match.Id, out var result))
		            {
			            result = match;
			            result.TeamTerrorists = new List<MatchHistoryPlayerDto>();
			            result.TeamCounterTerrorists = new List<MatchHistoryPlayerDto>();
			            lookup.Add(result.Id, result);
		            }

		            var playerDto = new MatchHistoryPlayerDto
		            {
			            Id = player.User_Id,
			            Username = player.User_Username,
			            Kills = player.User_Kills,
			            Assists = player.User_Assists,
			            Deaths = player.User_Deaths,
			            Score = player.User_Score
		            };

		            if (result.ScoreT == playerDto.Kills)
		            {
			            result.TeamTerrorists.Add(playerDto);
		            }
		            else
		            {
			            result.TeamCounterTerrorists.Add(playerDto);
		            }
		            
		            return result;
	            },
	            new {request.UserId},
	            splitOn: "User_Id");

            return lookup.Values;
        }
        
		private class MatchHistoryPlayerQueryResult
		{
			public Guid User_Id { get; init; }
			public string User_Username { get; init; }
			public int User_Kills { get; init; }
			public int User_Assists { get; init; }
			public int User_Deaths { get; init; }
			public int User_Score { get; init; }
		}
    }
}