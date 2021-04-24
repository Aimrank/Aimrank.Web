using Aimrank.Web.Common.Application.Data;
using Aimrank.Web.Common.Application.Exceptions;
using Aimrank.Web.Modules.Matches.Domain.Matches;
using Aimrank.Web.Modules.Matches.Domain.Players;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Domain.Matches
{
    internal class MatchRepository : IMatchRepository
    {
        private readonly MatchesContext _context;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public MatchRepository(MatchesContext context, ISqlConnectionFactory sqlConnectionFactory)
        {
            _context = context;
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Dictionary<PlayerId, int>> BrowsePlayersRatingAsync(IEnumerable<PlayerId> ids, MatchMode mode)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = @"
                SELECT DISTINCT
                    p.player_id,
                    FIRST_VALUE(p.rating_end) OVER (
                        PARTITION BY p.player_id
                        ORDER BY m.finished_at DESC
                    ) AS rating
                FROM matches.matches AS m
                INNER JOIN matches.matches_players AS p ON p.match_id = m.id
                WHERE
                    m.mode = @Mode AND
                    m.status = @Status AND
                    p.player_id IN @PlayerIds;";

            var result = new Dictionary<PlayerId, int>();
            
            await connection.QueryAsync<Guid, int, int>(sql,
                (playerId, rating) =>
                {
                    result[new PlayerId(playerId)] = rating;
                    return rating;
                },
                new
                {
                    Mode = mode,
                    Status = MatchStatus.Finished,
                    PlayerIds = ids.Select(id => id.Value)
                },
                splitOn: "rating");

            return result;
        }

        public async Task<Match> GetByIdAsync(MatchId id)
        {
            var match = await _context.Matches.FirstOrDefaultAsync(m => m.Id == id);
            if (match is null)
            {
                throw new EntityNotFoundException();
            }

            return match;
        }

        public void Add(Match match) => _context.Matches.Add(match);

        public void Delete(Match match) => _context.Matches.Remove(match);
    }
}