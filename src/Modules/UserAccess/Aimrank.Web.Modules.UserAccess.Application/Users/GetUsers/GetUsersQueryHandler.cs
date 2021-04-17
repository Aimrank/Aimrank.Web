using Aimrank.Web.Common.Application.Data;
using Aimrank.Web.Common.Application.Queries;
using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using Aimrank.Web.Modules.UserAccess.Application.Users.GetUserBatch;
using Dapper;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.UserAccess.Application.Users.GetUsers
{
    internal class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, PaginationDto<UserDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetUsersQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<PaginationDto<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            var username = $"{request.Name}%";
            
            const string sqlCount = @"
                SELECT COUNT (*)
                FROM [users].[Users] AS [U]
                WHERE [U].[Username] LIKE @Username;";

            const string sql = @"
                SELECT
                    [U].[Id] AS [Id],
                    [U].[Username] AS [Username]
                FROM [users].[Users] AS [U]
                WHERE [U].[Username] LIKE @Username
                ORDER BY [U].[Username]
                OFFSET @Offset ROWS FETCH NEXT @Fetch ROWS ONLY;";

            var count = await connection.ExecuteScalarAsync<int>(sqlCount, new {Username = username});

            var items = request.Pagination.Take > 0
                ? await connection.QueryAsync<UserDto>(sql, new
                {
                    Username = username,
                    Offset = request.Pagination.Skip,
                    Fetch = request.Pagination.Take
                })
                : Enumerable.Empty<UserDto>();

            return new PaginationDto<UserDto>(items, count);
        }
    }
}