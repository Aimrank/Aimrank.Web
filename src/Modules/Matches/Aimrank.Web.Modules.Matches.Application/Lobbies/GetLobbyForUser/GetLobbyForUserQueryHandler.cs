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
                DECLARE @id AS NVARCHAR(450);

                SELECT @id = [LobbyId]
                FROM [matches].[LobbiesMembers]
                WHERE [PlayerId] = @PlayerId;

                SELECT
                    [L].[Id] AS [Id],
                    [L].[Status] AS [Status],
                    [L].[Configuration_Maps] AS [Maps],
                    [L].[Configuration_Name] AS [Name],
                    [L].[Configuration_Mode] AS [Mode],
                    [M].[PlayerId] AS [PlayerId],
                    CASE [M].[Role]
                        WHEN 0 THEN 0
                        WHEN 1 THEN 1
                    END AS [IsLeader]
                FROM [matches].[Lobbies] AS [L]
                LEFT JOIN [matches].[LobbiesMembers] AS [M] ON [L].[Id] = [M].[LobbyId]
                WHERE [L].[Id] = @id;";

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
                splitOn: "Maps,PlayerId");

            return lookup.Values.FirstOrDefault();
        }
    }
}