using Aimrank.Web.Common.Application.Data;
using Aimrank.Web.Modules.Matches.Application.Contracts;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.Modules.Matches.Application.Lobbies.GetLobbyMembers
{
    internal class GetLobbyMembersQueryHandler : IQueryHandler<GetLobbyMembersQuery, IEnumerable<Guid>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetLobbyMembersQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public Task<IEnumerable<Guid>> Handle(GetLobbyMembersQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = @"
                SELECT player_id
                FROM matches.lobbies_members
                WHERE lobby_id = @LobbyId;";

            return connection.QueryAsync<Guid>(sql, new {request.LobbyId});
        }
    }
}