using Aimrank.Modules.Matches.Application.CSGO;
using Aimrank.Modules.Matches.Application.Contracts;
using Aimrank.Modules.Matches.Application.Lobbies.ProcessLobbies.Matchmaking;
using Aimrank.Modules.Matches.Domain.Lobbies;
using Aimrank.Modules.Matches.Domain.Matches;
using Aimrank.Modules.Matches.Domain.Players;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

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
            var lobbies = (await GetLobbies()).ToList();
            var players = (await GetPlayers(lobbies)).ToDictionary(p => p.Id);

            var manager = await MatchmakingManager.CreateAsync(_matchRepository, lobbies, players);

            var matches = manager.CreateMatches();

            foreach (var match in matches)
            {
                if (!_serverProcessManager.TryCreateReservation(match.Id))
                {
                    break;
                }

                var lobbiesToClose = lobbies.Where(l => match.Lobbies.Any(entry => entry.LobbyId == l.Id));

                foreach (var lobbyToClose in lobbiesToClose)
                {
                    lobbyToClose.Close();
                }

                _matchRepository.Add(match);
            }

            return Unit.Value;
        }

        private Task<IEnumerable<Lobby>> GetLobbies() => _lobbyRepository.BrowseByStatusAsync(LobbyStatus.Searching);

        private Task<IEnumerable<Player>> GetPlayers(IEnumerable<Lobby> lobbies)
            => _playerRepository.BrowseByIdAsync(lobbies.SelectMany(l => l.Members.Select(m => m.PlayerId)));
    }
}