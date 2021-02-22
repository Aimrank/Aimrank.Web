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
				WHERE [M].[Id] IN (
					SELECT TOP 30 [M2].[Id]
					FROM [aimrank].[Matches] AS [M2]
					INNER JOIN [aimrank].[MatchesPlayers] AS [P] on [M2].[Id] = [P].[MatchId]
					WHERE
						[P].[UserId] = @UserId AND
						[M2].[Status] = 5
					ORDER BY [M2].[FinishedAt] DESC
				)";

            const string sqlWhereStatus = "WHERE [M].[Status] = 5";

            var conditions = request.UserId.HasValue ? sqlWhereUser : sqlWhereStatus;
            
            var sql = @$"
				WITH [Result] AS (
					SELECT
						DENSE_RANK() OVER(ORDER BY [M].[FinishedAt] DESC) AS [Row],
						[M].[Id] AS [Id],
						[M].[ScoreT] AS [ScoreT],
						[M].[ScoreCT] AS [ScoreCT],
						[M].[Mode] AS [Mode],
						[M].[CreatedAt] AS [CreatedAt],
						[M].[FinishedAt] AS [FinishedAt],
						[M].[Map] AS [Map],
						[U].[Id] AS [User_Id],
						[U].[UserName] AS [User_Username],
						[P].[Team] AS [User_Team],
						[P].[Stats_Kills] AS [User_Kills],
						[P].[Stats_Assists] AS [User_Assists],
						[P].[Stats_Deaths] AS [User_Deaths],
						[P].[Stats_Score] AS [User_Score],
						[P].[RatingStart] AS [User_RatingStart],
						[P].[RatingEnd] AS [User_RatingEnd]
					FROM [aimrank].[Matches] AS [M]
					INNER JOIN [aimrank].[MatchesPlayers] AS [P] ON [M].[Id] = [P].[MatchId]
					INNER JOIN [aimrank].[AspNetUsers] AS [U] ON [P].[UserId] = [U].[Id]
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
			            Team = player.User_Team,
			            Kills = player.User_Kills,
			            Assists = player.User_Assists,
			            Deaths = player.User_Deaths,
			            Score = player.User_Score,
			            RatingStart = player.User_RatingStart,
			            RatingEnd = player.User_RatingEnd
		            };

		            if (playerDto.Team == 2)
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
			public int User_Team { get; set; }
			public int User_Kills { get; init; }
			public int User_Assists { get; init; }
			public int User_Deaths { get; init; }
			public int User_Score { get; init; }
			public int User_RatingStart { get; set; }
			public int User_RatingEnd { get; set; }
		}
    }
}