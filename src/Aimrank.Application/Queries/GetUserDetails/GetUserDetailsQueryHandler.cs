using Aimrank.Application.Contracts;
using Aimrank.Common.Application.Data;
using Dapper;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Queries.GetUserDetails
{
    public class GetUserDetailsQueryHandler : IQueryHandler<GetUserDetailsQuery, UserDetailsDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetUserDetailsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<UserDetailsDto> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql =
                @"SELECT
                    [User].[Id] AS [UserId],
                    [User].[SteamId] as [SteamId],
                    [User].[UserName] as [Username]
                  FROM [aimrank].[AspNetUsers] AS [User]
                  WHERE [User].[Id] = @UserId;";

            var details = await connection.QueryFirstOrDefaultAsync<UserDetailsDto>(sql, new {request.UserId});

            return details;
        }
    }
}