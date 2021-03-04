using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.Users.GetUserDetails;
using Aimrank.Common.Application.Data;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Queries.Friendships.GetBlockedUsers
{
    internal class GetBlockedUsersQueryHandler : IQueryHandler<GetBlockedUsersQuery, IEnumerable<UserDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetBlockedUsersQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public Task<IEnumerable<UserDto>> Handle(GetBlockedUsersQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

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
                    [F].[BlockingUserId2] = @UserId;";

            return connection.QueryAsync<UserDto>(sql, new {request.UserId});
        }
    }
}