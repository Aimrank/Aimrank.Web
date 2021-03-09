using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.Users.GetUserDetails;
using Aimrank.Common.Application.Data;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Application.Queries.Friendships.GetFriendship
{
    internal class GetFriendshipQueryHandler : IQueryHandler<GetFriendshipQuery, FriendshipDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetFriendshipQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<FriendshipDto> Handle(GetFriendshipQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            
            const string sql = @"
                SELECT
                    [U1].[Id] AS [User1_Id],
                    [U1].[UserName] AS [User1_Username],
                    [U1].[SteamId] AS [User1_SteamId],
                    [U2].[Id] AS [User2_Id],
                    [U2].[UserName] AS [User2_Username],
                    [U2].[SteamId] AS [User2_SteamId],
                    [F].[BlockingUserId1] AS [BlockingUserId1],
                    [F].[BlockingUserId2] AS [BlockingUserId2],
                    [F].[InvitingUserId] AS [InvitingUserId],
                    [F].[IsAccepted] AS [IsAccepted]
                FROM [aimrank].[Friendships] AS [F]
                INNER JOIN [aimrank].[AspNetUsers] AS [U1] ON [U1].[Id] = [F].[User1Id]
                INNER JOIN [aimrank].[AspNetUsers] AS [U2] ON [U2].[Id] = [F].[User2Id]
                WHERE
                    ([U1].[Id] = @UserId1 AND [U2].[Id] = @UserId2) OR
                    ([U1].[Id] = @UserId2 AND [U2].[Id] = @UserId1);";

            var result = await connection.QueryFirstAsync<FriendshipQueryResult>(sql,
                new {User1Id = request.UserId1, User2Id = request.UserId2});

            return result?.AsFriendshipDto();
        }

        private class FriendshipQueryResult
        {
            public Guid User1_Id { get; set; }
            public string User1_Username { get; set; }
            public string User1_SteamId { get; set; }
            public Guid User2_Id { get; set; }
            public string User2_Username { get; set; }
            public string User2_SteamId { get; set; }
            public Guid? BlockingUserId1 { get; set; }
            public Guid? BlockingUserId2 { get; set; }
            public Guid? InvitingUserId { get; set; }
            public bool IsAccepted { get; set; }

            public FriendshipDto AsFriendshipDto()
            {
                var result = new FriendshipDto
                {
                    IsAccepted = IsAccepted,
                    InvitingUserId = InvitingUserId,
                    User1 = new UserDto
                    {
                        Id = User1_Id,
                        SteamId = User1_SteamId,
                        Username = User1_Username
                    },
                    User2 = new UserDto
                    {
                        Id = User2_Id,
                        SteamId = User2_SteamId,
                        Username = User2_Username
                    }
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