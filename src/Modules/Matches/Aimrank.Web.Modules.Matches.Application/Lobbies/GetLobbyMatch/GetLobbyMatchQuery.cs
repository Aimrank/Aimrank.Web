using Aimrank.Web.Modules.Matches.Application.Contracts;
using System;

namespace Aimrank.Web.Modules.Matches.Application.Lobbies.GetLobbyMatch
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