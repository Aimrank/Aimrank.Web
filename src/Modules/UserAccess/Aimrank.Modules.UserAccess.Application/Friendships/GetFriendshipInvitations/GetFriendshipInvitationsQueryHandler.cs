using Aimrank.Common.Application.Data;
using Aimrank.Common.Application.Queries;
using Aimrank.Common.Application;
using Aimrank.Modules.UserAccess.Application.Contracts;
using Aimrank.Modules.UserAccess.Application.Users.GetUserBatch;
using Dapper;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Modules.UserAccess.Application.Friendships.GetFriendshipInvitations
{
    internal class GetFriendshipInvitationsQueryHandler : IQueryHandler<GetFriendshipInvitationsQuery, PaginationDto<UserDto>>
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetFriendshipInvitationsQueryHandler(
            IExecutionContextAccessor executionContextAccessor,
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _executionContextAccessor = executionContextAccessor;
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<PaginationDto<UserDto>> Handle(GetFriendshipInvitationsQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sqlCount = @"
                SELECT COUNT (*)
                FROM [users].[Friendships] AS [F]
                WHERE
                    [F].[IsAccepted] = 0 AND
                    [F].[BlockingUserId1] IS NULL AND
                    [F].[BlockingUserId2] IS NULL AND
                    [F].[InvitingUserId] <> @UserId AND
                    ([F].[User1Id] = @UserId OR [F].[User2Id] = @UserId);";
            
            const string sql = @"
                SELECT
                    CASE
                        WHEN [U1].[Id] = @UserId THEN [U2].[Id]
                        WHEN [U2].[Id] = @UserId THEN [U1].[Id]
                    END AS [Id],
                    CASE
                        WHEN [U1].[Id] = @UserId THEN [U2].[Username]
                        WHEN [U2].[Id] = @UserId THEN [U1].[Username]
                    END AS [Username]
                FROM [users].[Friendships] AS [F]
                INNER JOIN [users].[Users] AS [U1] ON [U1].[Id] = [F].[User1Id]
                INNER JOIN [users].[Users] AS [U2] ON [U2].[Id] = [F].[User2Id]
                WHERE
                    [F].[IsAccepted] = 0 AND
                    [F].[BlockingUserId1] IS NULL AND
                    [F].[BlockingUserId2] IS NULL AND
                    [F].[InvitingUserId] <> @UserId AND
                    ([F].[User1Id] = @UserId OR [F].[User2Id] = @UserId)
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