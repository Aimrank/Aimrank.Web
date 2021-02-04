using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.GetOpenedLobbies;
using Aimrank.Common.Application.Data;
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

        public GetLobbyForUserQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
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
                new {UserId = request.Id},
                splitOn: "UserId");

            return lookup.Values.FirstOrDefault();
        }
    }
}