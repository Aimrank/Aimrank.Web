using Aimrank.Modules.Matches.Domain.Lobbies;
using Aimrank.Modules.Matches.Domain.Matches;
using Aimrank.Modules.Matches.Domain.Players;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aimrank.Modules.Matches.Application.Lobbies.ProcessLobbies.Matchmaking
{
    internal class MatchmakingManager
    {
        private readonly Dictionary<MatchMode, List<SearchingLobby>> _buffer = new();
        private readonly Dictionary<PlayerId, Player> _players;
        private readonly Dictionary<PlayerId, int> _playersRatings;

        private MatchmakingManager(
            IEnumerable<Lobby> lobbies,
            Dictionary<PlayerId, Player> players,
            Dictionary<PlayerId, int> playersRatings)
        {
            _players = players;
            _playersRatings = playersRatings;

            foreach (var lobby in lobbies)
            {
                var mode = lobby.Configuration.Mode;
                
                if (!_buffer.TryGetValue(mode, out var list))
                {
                    list = new List<SearchingLobby>();
                    _buffer[mode] = list;
                }

                var rating = (int) lobby.Members.Select(m => _playersRatings[m.PlayerId]).Average();
                
                list.Add(new SearchingLobby(rating, lobby));
            }
        }
        
        public static async Task<MatchmakingManager> CreateAsync(
            IMatchRepository matchRepository,
            IEnumerable<Lobby> lobbies,
            Dictionary<PlayerId, Player> players)
        {
            var lobbiesArray = lobbies as Lobby[] ?? lobbies.ToArray();
            var modes = lobbiesArray.GroupBy(l => l.Configuration.Mode);

            var ratings = new Dictionary<PlayerId, int>();

            foreach (var mode in modes)
            {
                var modePlayers = mode.SelectMany(l => l.Members.Select(m => m.PlayerId)).ToList();
                var modeRatings = await matchRepository.BrowsePlayersRatingAsync(modePlayers, mode.Key);

                foreach (var id in modePlayers)
                {
                    ratings[id] = modeRatings.ContainsKey(id) ? modeRatings[id] : 1200;
                }
            }

            return new MatchmakingManager(lobbiesArray, players, ratings);
        }
        
        public IEnumerable<Match> CreateMatches()
        {
            var matches = new List<Match>();
            
            foreach (var (mode, entries) in _buffer)
            {
                var sorted = entries.OrderByDescending(e => e.Rating);
                var buffer = new SearchingLobbiesBuffer(mode, _players, _playersRatings);

                foreach (var entry in sorted)
                {
                    buffer.AddLobby(entry);

                    var match = buffer.GetMatch();
                    if (match is not null)
                    {
                        matches.Add(match);
                    }
                }
            }

            return matches;
        }
    }
}