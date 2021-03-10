using Aimrank.Application.Contracts;
using Aimrank.Common.Application.Data;
using Aimrank.Common.Application.Queries;
using Aimrank.Common.Application;
using Dapper;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Aimrank.Application.Queries.Users.GetUserBatch;

namespace Aimrank.Application.Queries.Friendships.GetBlockedUsers
{
    internal class GetBlockedUsersQueryHandler : IQueryHandler<GetBlockedUsersQuery, PaginationDto<UserDto>>
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetBlockedUsersQueryHandler(
            IExecutionContextAccessor executionContextAccessor,
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _executionContextAccessor = executionContextAccessor;
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<PaginationDto<UserDto>> Handle(GetBlockedUsersQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sqlCount = @"
                SELECT COUNT (*)
                FROM [aimrank].[Friendships] AS [F]
                WHERE
                    [F].[BlockingUserId1] = @UserId OR
                    [F].[BlockingUserId2] = @UserId";

            const string sql = @"
                SELECT
                    CASE
                        WHEN [U1].[Id] = @UserId THEN [U2].[Id]
                        WHEN [U2].[Id] = @UserId THEN [U1].[Id]
                    END AS [Id],
                    CASE
                        WHEN [U1].[Id] = @UserId THEN [U2].[UserName]
                        WHEN [U2].[Id] = @UserId THEN [U1].[UserName]
                    END AS [Username],
                    CASE
                        WHEN [U1].[Id] = @UserId THEN [U2].[SteamId]
                        WHEN [U2].[Id] = @UserId THEN [U1].[SteamId]
                    END AS [SteamId]
                FROM [aimrank].[Friendships] AS [F]
                INNER JOIN [aimrank].[AspNetUsers] AS [U1] ON [U1].[Id] = [F].[User1Id]
                INNER JOIN [aimrank].[AspNetUsers] AS [U2] ON [U2].[Id] = [F].[User2Id]
                WHERE
                    [F].[BlockingUserId1] = @UserId OR
                    [F].[BlockingUserId2] = @UserId
                ORDER BY [F].[CreatedAt] DESC
                OFFSET @Offset ROWS FETCH NEXT @Fetch ROWS ONLY;";

            var count = await connection.ExecuteScalarAsync<int>(sqlCount, new {_executionContextAccessor.UserId});

            var items = request.Pagination.Take > 0
                ? await connection.QueryAsync<UserDto>(sql, new
                {
                    _executionContextAccessor.UserId,
                    Offset = request.Pagination.Skip,
                    Fetch = request.Pagination.Take
                })
                : Enumerable.Empty<UserDto>();

            return new PaginationDto<UserDto>(items, count);
        }
    }
}