using Aimrank.Common.Application.Data;
using Aimrank.Modules.Matches.Application.Contracts;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Modules.Matches.Application.Lobbies.GetLobbyMembers
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
                SELECT [M].[PlayerId]
                FROM [matches].[LobbiesMembers] AS [M]
                WHERE [M].[LobbyId] = @LobbyId;";

            return connection.QueryAsync<Guid>(sql, new {request.LobbyId});
        }
    }
}