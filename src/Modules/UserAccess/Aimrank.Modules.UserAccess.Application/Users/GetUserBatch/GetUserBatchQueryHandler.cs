using Aimrank.Common.Application.Data;
using Aimrank.Modules.UserAccess.Application.Contracts;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Modules.UserAccess.Application.Users.GetUserBatch
{
    internal class GetUserBatchQueryHandler : IQueryHandler<GetUserBatchQuery, IEnumerable<UserDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetUserBatchQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetUserBatchQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = @"
                SELECT
                    [U].[Id] AS [Id],
                    [U].[Username] AS [Username]
                FROM [users].[Users] AS [U]
                WHERE [U].[Id] IN @UserIds;";

            var result = await connection.QueryAsync<UserDto>(sql, new {request.UserIds});

            var lookup = result.ToDictionary(u => u.Id);

            return request.UserIds.Select(id => lookup.GetValueOrDefault(id));
        }
    }
}