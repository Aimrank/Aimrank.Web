using Aimrank.Web.Common.Application.Data;
using Aimrank.Web.Common.Application;
using Aimrank.Web.Modules.Matches.Application.Contracts;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.Modules.Matches.Application.Lobbies.GetLobbyForUser
{
    internal class GetLobbyForUserQueryHandler : IQueryHandler<GetLobbyForUserQuery, LobbyDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IExecutionContextAccessor _executionContextAccessor;

        public GetLobbyForUserQueryHandler(
            ISqlConnectionFactory sqlConnectionFactory,
            IExecutionContextAccessor executionContextAccessor)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _executionContextAccessor = executionContextAccessor;
        }

        public async Task<LobbyDto> Handle(GetLobbyForUserQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            
            const string sql = @"
                SELECT
                    l.id,
                    l.status,
                    l.configuration_maps,
                    l.configuration_name,
                    l.configuration_mode,
                    m.player_id,
                    CASE m.role
                        WHEN 0 THEN 0
                        WHEN 1 THEN 1
                    END AS is_leader
                FROM matches.lobbies AS l
                LEFT JOIN matches.lobbies_members AS m ON l.id = m.lobby_id
                WHERE l.id = (
                    SELECT lobby_id
                    FROM matches.lobbies_members
                    WHERE player_id = @PlayerId
                );";

            var lookup = new Dictionary<Guid, LobbyDto>();
            
            await connection.QueryAsync<LobbyDto, LobbyConfigurationDto, LobbyMemberDto, LobbyDto>(
                sql,
                (details, configuration, member) =>
                {
                    if (!lookup.TryGetValue(details.Id, out var lobby))
                    {
                        lobby = new LobbyDto
                        {
                            Id = details.Id,
                            Status = details.Status,
                            Configuration = configuration,
                            Members = new List<LobbyMemberDto>()
                        };
                        
                        lookup.Add(lobby.Id, lobby);
                    }

                    if (member is not null)
                    {
                        lobby.Members.Add(member);
                    }

                    return lobby;
                },
                new {PlayerId = _executionContextAccessor.UserId},
                splitOn: "maps,player_id");

            return lookup.Values.FirstOrDefault();
        }
    }
}