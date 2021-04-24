using Aimrank.Web.Common.Application.Data;
using Aimrank.Web.Modules.Matches.Application.Contracts;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Application.Players.GetPlayerBatch
{
    internal class GetPlayerBatchQueryHandler : IQueryHandler<GetPlayerBatchQuery, IEnumerable<PlayerDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetPlayerBatchQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<IEnumerable<PlayerDto>> Handle(GetPlayerBatchQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = @"
                SELECT id, steam_id
                FROM matches.players
                WHERE id IN @PlayerIds;";

            var result = await connection.QueryAsync<PlayerDto>(sql, new {request.PlayerIds});

            var lookup = result.ToDictionary(p => p.Id);

            return request.PlayerIds.Select(id => lookup.GetValueOrDefault(id));
        }
    }
}