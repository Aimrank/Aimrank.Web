using Aimrank.Application.Contracts;
using Aimrank.Common.Application.Data;
using Dapper;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Queries.GetMatch
{
    public class GetMatchQueryHandler : IQueryHandler<GetMatchQuery, MatchDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMatchQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<MatchDto> Handle(GetMatchQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql =
                @"SELECT
                    [Match].[Id] AS [Id],
                    [Match].[Map] AS [Map],
                    [Match].[Status] AS [Status],
                    [Match].[Address] AS [Address]
                  FROM [aimrank].[Matches] AS [Match]
                  WHERE [Match].[Id] = @MatchId;";

            return await connection.QueryFirstAsync<MatchDto>(sql, new {MatchId = request.Id});
        }
    }
}