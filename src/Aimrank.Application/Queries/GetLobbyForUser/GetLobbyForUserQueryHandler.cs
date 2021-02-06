using Aimrank.Application.Contracts;
using Aimrank.Common.Application.Data;
using Aimrank.Common.Application;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Application.Queries.GetLobbyForUser
{
    public class GetLobbyForUserQueryHandler : IQueryHandler<GetLobbyForUserQuery, LobbyDto>
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
            
            const string sql =
                @"DECLARE @id AS NVARCHAR(450);

                  SELECT @id = [LobbyId]
                  FROM [aimrank].[LobbiesMembers]
                  WHERE [UserId] = @UserId;

                  SELECT
                    [Lobby].[Id] AS [Id],
                    [Lobby].[MatchId] AS [MatchId],
                    [Lobby].[Status] AS [Status],
                    [Lobby].[Configuration_Map] AS [Map],
                    [Member].[UserId] AS [UserId],
                    CASE [Member].[Role]
                        WHEN 0 THEN 0
                        WHEN 1 THEN 1
                    END AS [IsLeader]
                  FROM [aimrank].[Lobbies] AS [Lobby]
                  LEFT JOIN [aimrank].[LobbiesMembers] AS [Member] ON [Lobby].[Id] = [Member].[LobbyId]
                  WHERE [Lobby].[Id] = @id;";

            var lookup = new Dictionary<Guid, LobbyDto>();
            
            await connection.QueryAsync<LobbyDto, LobbyMemberDto, LobbyDto>(
                sql,
                (details, member) =>
                {
                    if (!lookup.TryGetValue(details.Id, out var lobby))
                    {
                        lobby = new LobbyDto
                        {
                            Id = details.Id,
                            MatchId = details.MatchId,
                            Map = details.Map,
                            Status = details.Status,
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
                new {_executionContextAccessor.UserId},
                splitOn: "UserId");

            return lookup.Values.FirstOrDefault();
        }
    }
}