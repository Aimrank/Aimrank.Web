using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.GetUserDetails;
using Aimrank.Common.Application.Data;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Queries.GetUsers
{
    public class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, IEnumerable<UserDetailsDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetUsersQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<IEnumerable<UserDetailsDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = @"
                SELECT TOP(20)
                    [U].[Id] AS [UserId],
                    [U].[SteamId] AS [SteamId],
                    [U].[UserName] AS [Username]
                FROM [aimrank].[AspNetUsers] AS [U]
                WHERE [U].[UserName] LIKE @Username
                ORDER BY [U].[UserName];";

            return await connection.QueryAsync<UserDetailsDto>(sql, new {Username = $"{request.Name}%"});
        }
    }
}