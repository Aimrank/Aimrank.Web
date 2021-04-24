using Aimrank.Web.Common.Application.Data;
using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.UserAccess.Application.Users.GetUserBatch
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
                SELECT id, username
                FROM users.users
                WHERE id = ANY(@UserIds);";

            var result = await connection.QueryAsync<UserDto>(sql, new {request.UserIds});

            var lookup = result.ToDictionary(u => u.Id);

            return request.UserIds.Select(id => lookup.GetValueOrDefault(id));
        }
    }
}