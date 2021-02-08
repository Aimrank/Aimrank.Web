using Aimrank.Application.Contracts;
using System;

namespace Aimrank.Application.Queries.GetMatchForLobby
{
    public class GetMatchForLobbyQuery : IQuery<MatchDto>
    {
        public Guid LobbyId { get; }

        public GetMatchForLobbyQuery(Guid lobbyId)
        {
            LobbyId = lobbyId;
        }
    }
}