using Aimrank.Web.Common.Application.Data;
using Aimrank.Web.Modules.Matches.Application.Contracts;
using Dapper;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Application.Lobbies.GetLobbyMatch
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
                SELECT m.id, m.map, m.mode, m.status, m.address
                FROM matches.matches AS m
                LEFT JOIN matches.matches_lobbies AS l ON l.match_id = m.id
                WHERE l.lobby_id = @LobbyId;";

            return await connection.QueryFirstOrDefaultAsync<LobbyMatchDto>(sql, new {request.LobbyId});
        }
    }
}