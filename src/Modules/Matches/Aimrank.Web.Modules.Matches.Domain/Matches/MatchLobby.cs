using Aimrank.Web.Common.Domain;
using Aimrank.Web.Modules.Matches.Domain.Lobbies;
using System.Collections.Generic;

namespace Aimrank.Web.Modules.Matches.Domain.Matches
{
    public class MatchLobby : ValueObject
    {
        public LobbyId LobbyId { get; }
        
        public MatchLobby(LobbyId lobbyId)
        {
            LobbyId = lobbyId;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return LobbyId;
        }
    }
}