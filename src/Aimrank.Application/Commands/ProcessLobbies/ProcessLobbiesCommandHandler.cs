using Aimrank.Application.CSGO;
using Aimrank.Application.Contracts;
using Aimrank.Domain.Lobbies;
using Aimrank.Domain.Matches;
using Aimrank.Domain.Users;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Application.Commands.ProcessLobbies
{
    public class ProcessLobbiesCommandHandler : ICommandHandler<ProcessLobbiesCommand>
    {
        private readonly ILobbyRepository _lobbyRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IUserRepository _userRepository;
        private readonly IServerProcessManager _serverProcessManager;

        public ProcessLobbiesCommandHandler(
            ILobbyRepository lobbyRepository,
            IMatchRepository matchRepository,
            IUserRepository userRepository,
            IServerProcessManager serverProcessManager)
        {
            _lobbyRepository = lobbyRepository;
            _matchRepository = matchRepository;
            _userRepository = userRepository;
            _serverProcessManager = serverProcessManager;
        }

        public async Task<Unit> Handle(ProcessLobbiesCommand request, CancellationToken cancellationToken)
        {
            var lobbiesSearching = await _lobbyRepository.BrowseByStatusAsync(LobbyStatus.Searching);
            var usersIds = lobbiesSearching.SelectMany(l => l.Members.Select(m => m.UserId));
            var users = (await _userRepository.BrowseByIdAsync(usersIds)).ToDictionary(u => u.Id);

            var lobbiesWithMode = GetLobbiesWithMode(lobbiesSearching);
            
            foreach (var (mode, lobbies) in lobbiesWithMode)
            {
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

                        var match = new Match(new MatchId(Guid.NewGuid()), lobby.Configuration.Map, mode, lobbiesIds);

                        var teams = new[]
                        {
                            new List<User>(),
                            new List<User>()
                        };

                        for (var i = 0; i < buffer.Count; i++)
                        {
                            foreach (var member in buffer[i].Members)
                            {
                                teams[i % 2].Add(users[member.UserId]);
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
                            foreach (var user in teams[j])
                            {
                                var rating = await _matchRepository.GetPlayerRatingAsync(user.Id, match.Mode);
                                
                                match.AddPlayer(user.Id, user.SteamId, j % 2 == 0 ? MatchTeam.T : MatchTeam.CT, rating);
                            }
                        }
                        
                        match.SetReady();

                        var lobbiesToClose = await _lobbyRepository.BrowseByIdAsync(lobbiesIds);

                        foreach (var lobbyToClose in lobbiesToClose)
                        {
                            lobbyToClose.Close();
                        }
                        
                        _serverProcessManager.CreateReservation(match.Id);
                        
                        _matchRepository.Add(match);
                        _lobbyRepository.UpdateRange(lobbiesToClose);
                        
                        buffer.Clear();
                    }
                }
            }
            
            return Unit.Value;
        }
        
        private static Dictionary<MatchMode, List<Lobby>> GetLobbiesWithMode(IEnumerable<Lobby> lobbies)
        {
            var result = new Dictionary<MatchMode, List<Lobby>>();
        
            foreach (var lobby in lobbies)
            {
                if (!result.TryGetValue(lobby.Configuration.Mode, out var list))
                {
                    list = new List<Lobby>();
                    result.Add(lobby.Configuration.Mode, list);
                }
                
                list.Add(lobby);
            }
        
            return result;
        }
    }
}