using Aimrank.Application.Contracts;
using Aimrank.Common.Application.Data;
using Dapper;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Queries.Users.GetUserDetails
{
    public class GetUserDetailsQueryHandler : IQueryHandler<GetUserDetailsQuery, UserDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetUserDetailsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<UserDto> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql =
                @"SELECT
                    [User].[Id] AS [UserId],
                    [User].[SteamId] as [SteamId],
                    [User].[UserName] as [Username]
                  FROM [aimrank].[AspNetUsers] AS [User]
                  WHERE [User].[Id] = @UserId;";

            return await connection.QueryFirstOrDefaultAsync<UserDto>(sql, new {request.UserId});
        }
    }
}