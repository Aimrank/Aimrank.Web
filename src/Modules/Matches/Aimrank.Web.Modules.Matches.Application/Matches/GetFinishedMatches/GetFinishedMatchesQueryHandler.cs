using Aimrank.Web.Common.Application.Data;
using Aimrank.Web.Common.Application.Queries;
using Aimrank.Web.Modules.Matches.Application.Contracts;
using Aimrank.Web.Modules.Matches.Domain.Matches;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.Modules.Matches.Application.Matches.GetFinishedMatches
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
				FROM matches.matches AS mi
				INNER JOIN matches.matches_players AS pi on mi.id = pi.match_id
				WHERE
					pi.player_id = @PlayerId AND
					mi.status = @Status
					{(request.Filter.Mode.HasValue ? "AND mi.mode = @Mode " : "")}
					{(!string.IsNullOrEmpty(request.Filter.Map) ? "AND mi.map LIKE @Map" : "")}";

            var sqlCount = $"SELECT COUNT (mi.id) {sqlInner}";

            var sqlOuter = @$"
				SELECT
					m.id,
					m.winner,
					m.score_t,
					m.score_ct,
					m.mode,
					m.created_at,
					m.finished_at,
					m.map,
					p.player_id,
					p.team AS player_team,
					p.stats_kills AS player_kills,
					p.stats_assists AS player_assists,
					p.stats_deaths AS player_deaths,
					p.stats_hs AS player_hs,
					p.rating_start AS player_rating_start,
					p.rating_end AS player_rating_end
				FROM matches.matches AS m
				INNER JOIN matches.matches_players AS p ON m.id = p.match_id
				WHERE m.id IN (
					SELECT mi.id
					{sqlInner}
					ORDER BY mi.finished_at DESC
					OFFSET @Offset ROWS FETCH NEXT @Fetch ROWS ONLY
				)
				ORDER BY m.finished_at DESC;";

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
							RatingStart = player.Player_Rating_Start,
							RatingEnd = player.Player_Rating_End
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
					splitOn: "player_id");
	        }

            return new PaginationDto<MatchDto>(lookup.Values, count);
        }
    }
}