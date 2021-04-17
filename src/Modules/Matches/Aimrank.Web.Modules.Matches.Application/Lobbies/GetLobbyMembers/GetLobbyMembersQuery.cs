using Aimrank.Web.Modules.Matches.Application.Contracts;
using System.Collections.Generic;
using System;

namespace Aimrank.Web.Modules.Matches.Application.Lobbies.GetLobbyMembers
{
    public class GetLobbyMembersQuery : IQuery<IEnumerable<Guid>>
    {
        public Guid LobbyId { get; }

        public GetLobbyMembersQuery(Guid lobbyId)
        {
            LobbyId = lobbyId;
        }
    }
}