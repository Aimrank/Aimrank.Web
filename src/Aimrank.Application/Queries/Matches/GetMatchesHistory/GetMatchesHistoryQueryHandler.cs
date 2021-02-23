using Aimrank.Application.Contracts;
using Aimrank.Common.Application.Data;
using Aimrank.Common.Application.Queries;
using Aimrank.Domain.Matches;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Application.Queries.Matches.GetMatchesHistory
{
    public class GetMatchesHistoryQueryHandler : IQueryHandler<GetMatchesHistoryQuery, PaginationDto<MatchHistoryDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMatchesHistoryQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<PaginationDto<MatchHistoryDto>> Handle(GetMatchesHistoryQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            var sqlParams = new
            {
	            request.UserId,
	            request.Pagination.Offset,
	            request.Pagination.Fetch,
	            request.Filter.Map,
	            request.Filter.Mode,
	            Status = MatchStatus.Finished
            };

            var sqlInner = $@"
				FROM [aimrank].[Matches] AS [MI]
				INNER JOIN [aimrank].[MatchesPlayers] AS [PI] on [MI].[Id] = [PI].[MatchId]
				WHERE
					[PI].[UserId] = @UserId AND
					[MI].[Status] = @Status
					{(request.Filter.Mode.HasValue ? "AND [MI].[Mode] = @Mode " : "")}
					{(!string.IsNullOrEmpty(request.Filter.Map) ? "AND [MI].[Map] LIKE @Map" : "")}";

            var sqlCount = $"SELECT COUNT ([MI].[Id]) {sqlInner}";
            
            var sqlOuter = @$"
				WITH [Result] AS (
					SELECT
						DENSE_RANK() OVER(ORDER BY [M].[FinishedAt] DESC) + 1 AS [Row],
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
					WHERE [M].[Id] IN (
						SELECT [MI].[Id]
						{sqlInner}
						ORDER BY [MI].[FinishedAt] DESC
						OFFSET @Offset ROWS FETCH NEXT @Fetch ROWS ONLY
					)
				)
				SELECT * FROM [Result] WHERE [Row] BETWEEN @Offset AND (@Offset + @Fetch);";

            var count = await connection.ExecuteScalarAsync<int>(sqlCount, sqlParams);

            var lookup = new Dictionary<Guid, MatchHistoryDto>();

            await connection.QueryAsync<MatchHistoryDto, MatchHistoryPlayerQueryResult, MatchHistoryDto>(
	            sqlOuter,
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
	            sqlParams,
	            splitOn: "User_Id");

            return new PaginationDto<MatchHistoryDto>(lookup.Values, count);
        }
    }
}