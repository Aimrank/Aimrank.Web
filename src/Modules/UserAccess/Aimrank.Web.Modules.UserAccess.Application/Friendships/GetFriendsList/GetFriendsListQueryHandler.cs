using Aimrank.Web.Common.Application.Data;
using Aimrank.Web.Common.Application.Queries;
using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using Dapper;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.Modules.UserAccess.Application.Friendships.GetFriendsList
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
                FROM users.friendships
                WHERE
                    is_accepted = 1 AND
                    (blocking_user_id_1 IS NULL OR blocking_user_id_1 <> @UserId) AND
                    (blocking_user_id_2 IS NULL OR blocking_user_id_2 <> @UserId) AND
                    (user_1_id = @UserId OR user_2_id = @UserId);";

            const string sql = @"
                SELECT
                    CASE
                        WHEN f.user_1_id = @UserId THEN f.user_2_id
                        WHEN f.user_2_id = @UserId THEN f.user_1_id
                    END AS id
                FROM users.friendships AS f
                WHERE
                    f.is_accepted = 1 AND
                    (f.blocking_user_id_1 IS NULL OR f.blocking_user_id_1 <> @UserId) AND
                    (f.blocking_user_id_2 IS NULL OR f.blocking_user_id_2 <> @UserId) AND
                    (f.user_1_id = @UserId OR f.user_2_id = @UserId)
                ORDER BY f.created_at DESC
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