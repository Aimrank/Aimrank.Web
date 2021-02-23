using Aimrank.Application.Contracts;
using System;

namespace Aimrank.Application.Queries.Matches.GetMatchForLobby
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