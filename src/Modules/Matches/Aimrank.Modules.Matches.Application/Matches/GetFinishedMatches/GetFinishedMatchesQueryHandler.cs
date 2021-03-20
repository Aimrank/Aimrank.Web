using Aimrank.Common.Application.Data;
using Aimrank.Common.Application.Queries;
using Aimrank.Modules.Matches.Application.Contracts;
using Aimrank.Modules.Matches.Domain.Matches;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Modules.Matches.Application.Matches.GetFinishedMatches
{
    internal class GetFinishedMatchesQueryHandler : IQueryHandler<GetFinishedMatchesQuery, PaginationDto<MatchDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetFinishedMatchesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<PaginationDto<MatchDto>> Handle(GetFinishedMatchesQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            var sqlParams = new
            {
	            request.Filter.PlayerId,
	            request.Filter.Map,
	            request.Filter.Mode,
	            Offset = request.Pagination.Skip,
	            Fetch = request.Pagination.Take,
	            Status = MatchStatus.Finished
            };

            var sqlInner = $@"
				FROM [matches].[Matches] AS [MI]
				INNER JOIN [matches].[MatchesPlayers] AS [PI] on [MI].[Id] = [PI].[MatchId]
				WHERE
					[PI].[PlayerId] = @PlayerId AND
					[MI].[Status] = @Status
					{(request.Filter.Mode.HasValue ? "AND [MI].[Mode] = @Mode " : "")}
					{(!string.IsNullOrEmpty(request.Filter.Map) ? "AND [MI].[Map] LIKE @Map" : "")}";

            var sqlCount = $"SELECT COUNT ([MI].[Id]) {sqlInner}";

            var sqlOuter = @$"
				SELECT
					[M].[Id] AS [Id],
					[M].[Winner] AS [Winner],
					[M].[ScoreT] AS [ScoreT],
					[M].[ScoreCT] AS [ScoreCT],
					[M].[Mode] AS [Mode],
					[M].[CreatedAt] AS [CreatedAt],
					[M].[FinishedAt] AS [FinishedAt],
					[M].[Map] AS [Map],
					[P].[PlayerId] AS [Player_Id],
					[P].[Team] AS [Player_Team],
					[P].[Stats_Kills] AS [Player_Kills],
					[P].[Stats_Assists] AS [Player_Assists],
					[P].[Stats_Deaths] AS [Player_Deaths],
					[P].[Stats_Hs] AS [Player_Hs],
					[P].[RatingStart] AS [Player_RatingStart],
					[P].[RatingEnd] AS [Player_RatingEnd]
				FROM [matches].[Matches] AS [M]
				INNER JOIN [matches].[MatchesPlayers] AS [P] ON [M].[Id] = [P].[MatchId]
				WHERE [M].[Id] IN (
					SELECT [MI].[Id]
					{sqlInner}
					ORDER BY [MI].[FinishedAt] DESC
					OFFSET @Offset ROWS FETCH NEXT @Fetch ROWS ONLY
				);";

            var count = await connection.ExecuteScalarAsync<int>(sqlCount, sqlParams);

            var lookup = new Dictionary<Guid, MatchDto>();

            if (sqlParams.Fetch > 0)
            {
				await connection.QueryAsync<MatchDto, MatchPlayerQueryResult, MatchDto>(
					sqlOuter,
					(match, player) =>
					{
						if (!lookup.TryGetValue(match.Id, out var result))
						{
							result = match;
							result.TeamTerrorists = new List<MatchPlayerDto>();
							result.TeamCounterTerrorists = new List<MatchPlayerDto>();
							lookup.Add(result.Id, result);
						}

						var playerDto = new MatchPlayerDto
						{
							Id = player.Player_Id,
							Team = player.Player_Team,
							Kills = player.Player_Kills,
							Assists = player.Player_Assists,
							Deaths = player.Player_Deaths,
							Hs = player.Player_Hs,
							RatingStart = player.Player_RatingStart,
							RatingEnd = player.Player_RatingEnd
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
					splitOn: "Player_Id");
	        }

            return new PaginationDto<MatchDto>(lookup.Values, count);
        }
    }
}