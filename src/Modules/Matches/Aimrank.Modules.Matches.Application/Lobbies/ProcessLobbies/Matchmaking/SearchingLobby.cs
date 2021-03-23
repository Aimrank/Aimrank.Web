using Aimrank.Modules.Matches.Domain.Lobbies;

namespace Aimrank.Modules.Matches.Application.Lobbies.ProcessLobbies.Matchmaking
{
    internal class SearchingLobby
    {
        public int Rating { get; }
        public Lobby Lobby { get; }

        public SearchingLobby(int rating, Lobby lobby)
        {
            Rating = rating;
            Lobby = lobby;
        }
    }
}