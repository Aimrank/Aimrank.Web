using Aimrank.Application.Contracts;
using Aimrank.Common.Application.Data;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Application.Queries.GetOpenedLobbies
{
    public class GetOpenedLobbiesQueryHandler : IQueryHandler<GetOpenedLobbiesQuery, IEnumerable<LobbyDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetOpenedLobbiesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<IEnumerable<LobbyDto>> Handle(GetOpenedLobbiesQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql =
                @"SELECT
                    [Lobby].[Id] AS [Id],
                    [Lobby].[Status] AS [Status],
                    [Lobby].[Configuration_Map] AS [Map],
                    [Member].[UserId] AS [UserId],
                    CASE [Member].[Role]
                        WHEN 0 THEN 0
                        WHEN 1 THEN 1
                    END AS [IsLeader]
                  FROM [aimrank].[Lobbies] AS [Lobby]
                  LEFT JOIN [aimrank].[LobbiesMembers] AS [Member] ON [Lobby].[Id] = [Member].[LobbyId]
                  WHERE [Lobby].[Status] = 0;";

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
                splitOn: "UserId");

            return lookup.Values;
        }
    }
}