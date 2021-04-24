using Aimrank.Web.Common.Application.Data;
using Aimrank.Web.Common.Application.Queries;
using Aimrank.Web.Common.Application;
using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using Aimrank.Web.Modules.UserAccess.Application.Users.GetUserBatch;
using Dapper;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.UserAccess.Application.Friendships.GetFriendshipInvitations
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
                FROM users.friendships
                WHERE
                    is_accepted = false AND
                    blocking_user_id_1 IS NULL AND
                    blocking_user_id_2 IS NULL AND
                    inviting_user_id <> @UserId AND
                    (user_1_id = @UserId OR user_2_id = @UserId);";
            
            const string sql = @"
                SELECT
                    CASE
                        WHEN u1.id = @UserId THEN u2.id
                        WHEN u2.id = @UserId THEN u1.id
                    END AS id,
                    CASE
                        WHEN u1.id = @UserId THEN u2.username
                        WHEN u2.id = @UserId THEN u1.username
                    END AS username
                FROM users.friendships AS f
                INNER JOIN users.users AS u1 ON u1.id = f.user_1_id
                INNER JOIN users.users AS u2 ON u2.id = f.user_2_id
                WHERE
                    f.is_accepted = false AND
                    f.blocking_user_id_1 IS NULL AND
                    f.blocking_user_id_2 IS NULL AND
                    f.inviting_user_id <> @UserId AND
                    (f.user_1_id = @UserId OR f.user_2_id = @UserId)
                ORDER BY f.created_at DESC
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