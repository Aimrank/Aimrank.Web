using Aimrank.Application.Contracts;
using Aimrank.Common.Application.Data;
using Aimrank.Common.Application.Queries;
using Dapper;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Application.Queries.Friendships.GetFriendsList
{
    internal class GetFriendsListQueryHandler : IQueryHandler<GetFriendsListQuery, PaginationDto<Guid>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetFriendsListQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<PaginationDto<Guid>> Handle(GetFriendsListQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            
            const string sqlCount = @"
                SELECT COUNT (*)
                FROM [aimrank].[Friendships] AS [F]
                WHERE
                    [F].[IsAccepted] = 1 AND
                    ([F].[BlockingUserId1] IS NULL OR [F].[BlockingUserId1] <> @UserId) AND
                    ([F].[BlockingUserId2] IS NULL OR [F].[BlockingUserId2] <> @UserId) AND
                    ([F].[User1Id] = @UserId OR [F].[User2Id] = @UserId);";

            const string sql = @"
                SELECT
                    CASE
                        WHEN [F].[User1Id] = @UserId THEN [F].[User2Id]
                        WHEN [F].[User2Id] = @UserId THEN [F].[User1Id]
                    END AS [Id]
                FROM [aimrank].[Friendships] AS [F]
                WHERE
                    [F].[IsAccepted] = 1 AND
                    ([F].[BlockingUserId1] IS NULL OR [F].[BlockingUserId1] <> @UserId) AND
                    ([F].[BlockingUserId2] IS NULL OR [F].[BlockingUserId2] <> @UserId) AND
                    ([F].[User1Id] = @UserId OR [F].[User2Id] = @UserId)
                ORDER BY [F].[CreatedAt] DESC
                OFFSET @Offset ROWS FETCH NEXT @Fetch ROWS ONLY;";
            
            var count = await connection.ExecuteScalarAsync<int>(sqlCount, new {request.UserId});

            var items = request.Pagination.Take > 0
                ? await connection.QueryAsync<Guid>(sql, new
                {
                    request.UserId,
                    Offset = request.Pagination.Skip,
                    Fetch = request.Pagination.Take
                })
                : Enumerable.Empty<Guid>();

            return new PaginationDto<Guid>(items, count);
        }
    }
}