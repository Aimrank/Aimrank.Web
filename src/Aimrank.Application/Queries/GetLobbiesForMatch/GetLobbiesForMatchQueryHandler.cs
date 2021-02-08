using Aimrank.Application.Contracts;
using Aimrank.Common.Application.Data;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Application.Queries.GetLobbiesForMatch
{
    public class GetLobbiesForMatchQueryHandler : IQueryHandler<GetLobbiesForMatchQuery, IEnumerable<Guid>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetLobbiesForMatchQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<IEnumerable<Guid>> Handle(GetLobbiesForMatchQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = @"
                SELECT
                    [L].[Id] AS [Id]
                FROM [aimrank].[Lobbies] AS [L]
                WHERE [L].[MatchId] = @MatchId;";

            return await connection.QueryAsync<Guid>(sql, new {request.MatchId});
        }
    }
}