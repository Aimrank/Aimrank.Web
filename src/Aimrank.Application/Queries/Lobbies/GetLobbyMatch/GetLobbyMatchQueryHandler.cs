using Aimrank.Application.Contracts;
using Aimrank.Common.Application.Data;
using Dapper;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Queries.Lobbies.GetLobbyMatch
{
    internal class GetLobbyMatchQueryHandler : IQueryHandler<GetLobbyMatchQuery, LobbyMatchDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetLobbyMatchQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<LobbyMatchDto> Handle(GetLobbyMatchQuery request, CancellationToken cancellationToken)
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

            return await connection.QueryFirstOrDefaultAsync<LobbyMatchDto>(sql, new {request.LobbyId});
        }
    }
}