using Aimrank.Common.Application.Data;
using Aimrank.Modules.Matches.Application.Contracts;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Modules.Matches.Application.Players.GetPlayerBatch
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
                SELECT
                    [P].[Id],
                    [P].[SteamId]
                FROM [matches].[Players] AS [P]
                WHERE [P].[Id] IN @PlayerIds;";

            var result = await connection.QueryAsync<PlayerDto>(sql, new {request.PlayerIds});

            var lookup = result.ToDictionary(p => p.Id);

            return request.PlayerIds.Select(id => lookup.GetValueOrDefault(id));
        }
    }
}