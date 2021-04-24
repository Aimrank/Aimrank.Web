using Aimrank.Web.Common.Application.Data;
using Aimrank.Web.Common.Application;
using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.Modules.UserAccess.Application.Friendships.GetFriendshipBatch
{
    internal class GetFriendshipBatchQueryHandler : IQueryHandler<GetFriendshipBatchQuery, IEnumerable<FriendshipDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IExecutionContextAccessor _executionContextAccessor;

        public GetFriendshipBatchQueryHandler(
            ISqlConnectionFactory sqlConnectionFactory,
            IExecutionContextAccessor executionContextAccessor)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _executionContextAccessor = executionContextAccessor;
        }

        public async Task<IEnumerable<FriendshipDto>> Handle(GetFriendshipBatchQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = @"
                SELECT
                    u1.id AS user_id_1,
                    u2.id AS user_id_2,
                    f.blocking_user_id_1 AS blocking_user_id_1,
                    f.blocking_user_id_2 AS blocking_user_id_2,
                    f.inviting_user_id AS inviting_user_id,
                    f.is_accepted AS is_accepted
                FROM users.friendships AS f
                INNER JOIN users.users AS u1 ON u1.id = f.user_1_id
                INNER JOIN users.users AS u2 ON u2.id = f.user_2_id
                WHERE
                    (u1.id IN @UserIds AND u2.id = @CurrentUserId) OR
                    (u2.id IN @userIds AND u1.id = @CurrentUserId);";

            var result = await connection.QueryAsync<FriendshipQueryResult>(sql,
                new {request.UserIds, CurrentUserId = _executionContextAccessor.UserId});

            var lookup = result.ToDictionary(
                f => f.UserId1 == _executionContextAccessor.UserId ? f.UserId2 : f.UserId1);

            return request.UserIds.Select(id => lookup.GetValueOrDefault(id)?.AsFriendshipDto());
        }

        private class FriendshipQueryResult
        {
            public Guid UserId1 { get; set; }
            public Guid UserId2 { get; set; }
            public Guid? BlockingUserId1 { get; set; }
            public Guid? BlockingUserId2 { get; set; }
            public Guid? InvitingUserId { get; set; }
            public bool IsAccepted { get; set; }

            public FriendshipDto AsFriendshipDto()
            {
                var result = new FriendshipDto
                {
                    UserId1 = UserId1,
                    UserId2 = UserId2,
                    IsAccepted = IsAccepted,
                    InvitingUserId = InvitingUserId,
                };

                var blockingUsersIds = new List<Guid>();
                
                if (BlockingUserId1 is not null) blockingUsersIds.Add(BlockingUserId1.Value);
                if (BlockingUserId2 is not null) blockingUsersIds.Add(BlockingUserId2.Value);

                result.BlockingUsersIds = blockingUsersIds;

                return result;
            }
        }
    }
}