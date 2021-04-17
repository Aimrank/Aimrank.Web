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
                    [U1].[Id] AS [UserId1],
                    [U2].[Id] AS [UserId2],
                    [F].[BlockingUserId1] AS [BlockingUserId1],
                    [F].[BlockingUserId2] AS [BlockingUserId2],
                    [F].[InvitingUserId] AS [InvitingUserId],
                    [F].[IsAccepted] AS [IsAccepted]
                FROM [users].[Friendships] AS [F]
                INNER JOIN [users].[Users] AS [U1] ON [U1].[Id] = [F].[User1Id]
                INNER JOIN [users].[Users] AS [U2] ON [U2].[Id] = [F].[User2Id]
                WHERE
                    ([U1].[Id] IN @UserIds AND [U2].[Id] = @CurrentUserId) OR
                    ([U2].[Id] IN @userIds AND [U1].[Id] = @CurrentUserId);";

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