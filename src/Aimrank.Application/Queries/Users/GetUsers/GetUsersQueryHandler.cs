using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.Users.GetUserBatch;
using Aimrank.Common.Application.Data;
using Aimrank.Common.Application.Queries;
using Dapper;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Queries.Users.GetUsers
{
    internal class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, PaginationDto<UserDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetUsersQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<PaginationDto<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            var username = $"{request.Name}%";
            
            const string sqlCount = @"
                SELECT COUNT (*)
                FROM [aimrank].[AspNetUsers] AS [U]
                WHERE [U].[UserName] LIKE @Username;";

            const string sql = @"
                SELECT
                    [U].[Id] AS [Id],
                    [U].[SteamId] AS [SteamId],
                    [U].[UserName] AS [Username]
                FROM [aimrank].[AspNetUsers] AS [U]
                WHERE [U].[UserName] LIKE @Username
                ORDER BY [U].[UserName]
                OFFSET @Offset ROWS FETCH NEXT @Fetch ROWS ONLY;";

            var count = await connection.ExecuteScalarAsync<int>(sqlCount, new {Username = username});

            var items = request.Pagination.Take > 0
                ? await connection.QueryAsync<UserDto>(sql, new
                {
                    Username = username,
                    Offset = request.Pagination.Skip,
                    Fetch = request.Pagination.Take
                })
                : Enumerable.Empty<UserDto>();

            return new PaginationDto<UserDto>(items, count);
        }
    }
}