using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.Users.GetUserDetails;
using Aimrank.Common.Application.Data;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Queries.Users.GetUsers
{
    internal class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, IEnumerable<UserDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetUsersQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = @"
                SELECT TOP(20)
                    [U].[Id] AS [Id],
                    [U].[SteamId] AS [SteamId],
                    [U].[UserName] AS [Username]
                FROM [aimrank].[AspNetUsers] AS [U]
                WHERE [U].[UserName] LIKE @Username
                ORDER BY [U].[UserName];";

            return await connection.QueryAsync<UserDto>(sql, new {Username = $"{request.Name}%"});
        }
    }
}