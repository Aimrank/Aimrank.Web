using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.GetOpenedLobbies;
using System;

namespace Aimrank.Application.Queries.GetLobbyForUser
{
    public class GetLobbyForUserQuery : IQuery<LobbyDto>
    {
        public Guid Id { get; }

        public GetLobbyForUserQuery(Guid id)
        {
            Id = id;
        }
    }
}