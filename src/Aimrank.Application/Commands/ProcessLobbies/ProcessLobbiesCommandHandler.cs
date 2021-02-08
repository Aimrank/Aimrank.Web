using Aimrank.Application.CSGO;
using Aimrank.Application.Contracts;
using Aimrank.Domain.Lobbies;
using Aimrank.Domain.Matches;
using Aimrank.Domain.Users;
using MediatR;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Application.Commands.ProcessLobbies
{
    public class ProcessLobbiesCommandHandler : ICommandHandler<ProcessLobbiesCommand>
    {
        private readonly ILobbyRepository _lobbyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IServerProcessManager _serverProcessManager;

        public ProcessLobbiesCommandHandler(
            ILobbyRepository lobbyRepository,
            IUserRepository userRepository,
            IMatchRepository matchRepository,
            IServerProcessManager serverProcessManager)
        {
            _lobbyRepository = lobbyRepository;
            _userRepository = userRepository;
            _matchRepository = matchRepository;
            _serverProcessManager = serverProcessManager;
        }

        public async Task<Unit> Handle(ProcessLobbiesCommand request, CancellationToken cancellationToken)
        {
            var lobbies = await _lobbyRepository.BrowseByStatusAsync(LobbyStatus.Searching);

            foreach (var lobby in lobbies)
            {
                if (lobby.Members.Count() != 2)
                {
                    continue;
                }

                var p1 = await _userRepository.GetByIdAsync(lobby.Members.ElementAt(0).UserId);
                var p2 = await _userRepository.GetByIdAsync(lobby.Members.ElementAt(1).UserId);

                var matchId = new MatchId(Guid.NewGuid());

                var match = new Match(
                    matchId,
                    lobby.Configuration.Map,
                    new MatchPlayer[]
                    {
                        new(p1.Id, p1.SteamId, MatchTeam.Terrorists),
                        new(p2.Id, p2.SteamId, MatchTeam.CounterTerrorists)
                    },
                    new MatchLobby[]
                    {
                        new(lobby.Id)
                    });

                var address = _serverProcessManager.StartServer(
                    match.Id.Value,
                    match.Players.Select(p => p.SteamId),
                    match.Map);
                
                lobby.MatchFound();
                lobby.StartMatch();
                
                // Todo: StartMatch should be invoked after members accept game
                
                match.Start(address);
                
                _matchRepository.Add(match);
                _lobbyRepository.Update(lobby);
            }
            
            return Unit.Value;
        }
    }
}