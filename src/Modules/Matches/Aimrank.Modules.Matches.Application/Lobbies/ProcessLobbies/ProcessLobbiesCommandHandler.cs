using Aimrank.Modules.Matches.Application.CSGO;
using Aimrank.Modules.Matches.Application.Contracts;
using Aimrank.Modules.Matches.Domain.Lobbies;
using Aimrank.Modules.Matches.Domain.Matches;
using Aimrank.Modules.Matches.Domain.Players;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Modules.Matches.Application.Lobbies.ProcessLobbies
{
    internal class ProcessLobbiesCommandHandler : ICommandHandler<ProcessLobbiesCommand>
    {
        private readonly ILobbyRepository _lobbyRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IServerProcessManager _serverProcessManager;

        public ProcessLobbiesCommandHandler(
            ILobbyRepository lobbyRepository,
            IMatchRepository matchRepository,
            IPlayerRepository playerRepository,
            IServerProcessManager serverProcessManager)
        {
            _lobbyRepository = lobbyRepository;
            _matchRepository = matchRepository;
            _playerRepository = playerRepository;
            _serverProcessManager = serverProcessManager;
        }

        public async Task<Unit> Handle(ProcessLobbiesCommand request, CancellationToken cancellationToken)
        {
            var lobbiesSearching = await _lobbyRepository.BrowseByStatusAsync(LobbyStatus.Searching);
            var lobbiesGrouped = GetLobbiesWithMode(lobbiesSearching);
            var lobbiesPlayers = await GetPlayersForLobbiesAsync(lobbiesSearching);
            
            foreach (var (mode, lobbies) in lobbiesGrouped)
            {
                var ratings = await GetRatingForPlayersAsync(lobbies, mode);
                
                var sorted = lobbies.OrderByDescending(l => l.Members.Count());

                var buffer = new List<Lobby>();
                
                foreach (var lobby in sorted)
                {
                    buffer.Add(lobby);

                    var playersCount = buffer.Sum(l => l.Members.Count());

                    if (playersCount == 2 && mode == MatchMode.OneVsOne ||
                        playersCount == 4 && mode == MatchMode.TwoVsTwo)
                    {
                        var lobbiesIds = buffer.Select(l => new LobbyId(l.Id));

                        var match = new Match(new MatchId(Guid.NewGuid()), lobby.GetMap(), mode, lobbiesIds);

                        var teams = new[]
                        {
                            new List<Player>(),
                            new List<Player>()
                        };

                        for (var i = 0; i < buffer.Count; i++)
                        {
                            foreach (var member in buffer[i].Members)
                            {
                                teams[i % 2].Add(lobbiesPlayers[member.PlayerId]);
                            }
                        }
                        
                        if (teams[0].Count != teams[1].Count)
                        {
                            var teamToIncrease = teams[0].Count > teams[1].Count ? teams[1] : teams[0];
                            var teamToReduce = teams[0].Count > teams[1].Count ? teams[0] : teams[1];

                            var diff = Math.Abs(teams[0].Count - teams[1].Count) / 2;
                            
                            teamToIncrease.AddRange(teamToReduce.Skip(teamToReduce.Count - diff));
                            teamToReduce.RemoveRange(teamToReduce.Count - diff, diff);
                        }

                        for (var j = 0; j < teams.Length; j++)
                        {
                            foreach (var player in teams[j])
                            {
                                var rating = ratings.ContainsKey(player.Id) ? ratings[player.Id] : 1200;
                                
                                match.AddPlayer(player.Id, player.SteamId, j % 2 == 0 ? MatchTeam.T : MatchTeam.CT, rating);
                            }
                        }
                        
                        match.SetReady();

                        var lobbiesToClose = lobbiesSearching.Where(l => lobbiesIds.Contains(l.Id));

                        foreach (var lobbyToClose in lobbiesToClose)
                        {
                            lobbyToClose.Close();
                        }
                        
                        _serverProcessManager.CreateReservation(match.Id);
                        
                        _matchRepository.Add(match);
                        
                        buffer.Clear();
                    }
                }
            }
            
            return Unit.Value;
        }
        
        private async Task<Dictionary<PlayerId, Player>> GetPlayersForLobbiesAsync(IEnumerable<Lobby> lobbies)
        {
            var playerIds = lobbies.SelectMany(l => l.Members.Select(m => m.PlayerId));
            var players = await _playerRepository.BrowseByIdAsync(playerIds);
            return players.ToDictionary(u => u.Id);
        }

        private async Task<Dictionary<PlayerId, int>> GetRatingForPlayersAsync(IEnumerable<Lobby> lobbies, MatchMode mode)
        {
            var members = lobbies.SelectMany(l => l.Members).Select(m => m.PlayerId).Distinct();
            return await _matchRepository.BrowsePlayersRatingAsync(members, mode);
        }
        
        private static Dictionary<MatchMode, List<Lobby>> GetLobbiesWithMode(IEnumerable<Lobby> lobbies)
        {
            var result = new Dictionary<MatchMode, List<Lobby>>();
        
            foreach (var lobby in lobbies)
            {
                if (!result.TryGetValue(lobby.GetMode(), out var list))
                {
                    list = new List<Lobby>();
                    result.Add(lobby.GetMode(), list);
                }
                
                list.Add(lobby);
            }
        
            return result;
        }
    }
}