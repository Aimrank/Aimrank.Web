using Aimrank.Application.Contracts;
using System;

namespace Aimrank.Application.Queries.Lobbies.GetLobbyMatch
{
    public class GetLobbyMatchQuery : IQuery<LobbyMatchDto>
    {
        public Guid LobbyId { get; }

        public GetLobbyMatchQuery(Guid lobbyId)
        {
            LobbyId = lobbyId;
        }
    }
}