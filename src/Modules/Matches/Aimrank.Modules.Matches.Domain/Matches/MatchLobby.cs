using Aimrank.Common.Domain;
using Aimrank.Modules.Matches.Domain.Lobbies;
using System.Collections.Generic;

namespace Aimrank.Modules.Matches.Domain.Matches
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