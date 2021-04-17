using System;
using System.Collections.Generic;
using System.Linq;
using Aimrank.Web.Modules.Matches.Domain.Lobbies;
using Aimrank.Web.Modules.Matches.Domain.Matches;
using Aimrank.Web.Modules.Matches.Domain.Players;

namespace Aimrank.Web.Modules.Matches.Application.Lobbies.ProcessLobbies.Matchmaking
{
    internal class SearchingLobbiesBuffer
    {
        private readonly List<SearchingLobby> _buffer = new();
        
        private readonly MatchMode _mode;
        private readonly Dictionary<PlayerId, Player> _players;
        private readonly Dictionary<PlayerId, int> _playersRatings;

        public SearchingLobbiesBuffer(
            MatchMode mode,
            Dictionary<PlayerId, Player> players,
            Dictionary<PlayerId, int> playersRatings)
        {
            _mode = mode;
            _players = players;
            _playersRatings = playersRatings;
        }

        public void AddLobby(SearchingLobby entry)
        {
            if (entry.Lobby.IsFull())
            {
                _buffer.Insert(0, entry);
            }
            else
            {
                _buffer.Add(entry);
            }
        }

        public Match GetMatch()
        {
            var maps = new Dictionary<string, List<Lobby>>();
            var lobbies = new List<Lobby>();

            Match match = null;

            foreach (var entry in _buffer)
            {
                foreach (var map in entry.Lobby.Configuration.Maps)
                {
                    if (!maps.TryGetValue(map, out var list))
                    {
                        list = new List<Lobby>();
                        maps[map] = list;
                    }
                    
                    list.Add(entry.Lobby);
                    
                    if (_mode == MatchMode.OneVsOne && list.Select(l => l.Members.Count()).Sum() == 2 ||
                        _mode == MatchMode.TwoVsTwo && list.Select(l => l.Members.Count()).Sum() == 4)
                    {
                        match = new Match(new MatchId(Guid.NewGuid()), map, _mode, list.Select(l => l.Id));
                        lobbies = list;
                        
                        var team1 = new List<PlayerId>();
                        var team2 = new List<PlayerId>();
                        
                        foreach (var lobby in list)
                        {
                            if (team1.Count < team2.Count)
                            {
                                team1.AddRange(lobby.Members.Select(m => m.PlayerId));
                            }
                            else
                            {
                                team2.AddRange(lobby.Members.Select(m => m.PlayerId));
                            }
                        }

                        var teamToIncrease = team1.Count < team2.Count ? team1 : team2;
                        var teamToDecrease = team1.Count > team2.Count ? team1 : team2;

                        if (teamToIncrease.Count != teamToDecrease.Count)
                        {
                            var playersToRemove = (teamToDecrease.Count - teamToIncrease.Count) / 2;
                            teamToIncrease.AddRange(teamToDecrease.GetRange(teamToDecrease.Count - playersToRemove, playersToRemove));
                            teamToDecrease.RemoveRange(teamToDecrease.Count - playersToRemove, playersToRemove);
                        }

                        foreach (var id in team1)
                        {
                            match.AddPlayer(id, _players[id].SteamId, MatchTeam.T, _playersRatings[id]);
                        }
                        
                        foreach (var id in team2)
                        {
                            match.AddPlayer(id, _players[id].SteamId, MatchTeam.CT, _playersRatings[id]);
                        }
                        
                        match.SetReady();
                        
                        break;
                    }
                }
            }

            _buffer.RemoveAll(e => lobbies.Any(lobby => lobby.Id == e.Lobby.Id));
            
            return match;
        }
    }
}