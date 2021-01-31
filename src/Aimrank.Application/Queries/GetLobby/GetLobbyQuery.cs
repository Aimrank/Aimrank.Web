using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.GetOpenedLobbies;
using System;

namespace Aimrank.Application.Queries.GetLobby
{
    public class GetLobbyQuery : IQuery<LobbyDto>
    {
        public Guid Id { get; }

        public GetLobbyQuery(Guid id)
        {
            Id = id;
        }
    }
}