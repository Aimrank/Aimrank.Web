using Aimrank.Application.Contracts;
using Aimrank.Common.Application.Data;
using Dapper;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Queries.Matches.GetMatchForLobby
{
    public class GetMatchForLobbyQueryHandler : IQueryHandler<GetMatchForLobbyQuery, MatchDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMatchForLobbyQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<MatchDto> Handle(GetMatchForLobbyQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = @"
                SELECT
                    [M].[Id] AS [Id],
                    [M].[Map] AS [Map],
                    [M].[Mode] AS [Mode],
                    [M].[Status] AS [Status],
                    [M].[Address] AS [Address]
                FROM [aimrank].[Matches] AS [M]
                LEFT JOIN [aimrank].[MatchesLobbies] AS [L] ON [L].[MatchId] = [M].[Id]
                WHERE [L].[LobbyId] = @LobbyId;";

            return await connection.QueryFirstOrDefaultAsync<MatchDto>(sql, new {request.LobbyId});
        }
    }
}