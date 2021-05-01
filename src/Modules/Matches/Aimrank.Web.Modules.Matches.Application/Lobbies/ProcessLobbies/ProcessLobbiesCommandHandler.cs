using Aimrank.Web.Modules.Matches.Application.Clients;
using Aimrank.Web.Modules.Matches.Application.Lobbies.ProcessLobbies.Matchmaking;
using Aimrank.Web.Modules.Matches.Domain.Lobbies;
using Aimrank.Web.Modules.Matches.Domain.Matches;
using Aimrank.Web.Modules.Matches.Domain.Players;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Application.Lobbies.ProcessLobbies
{
    internal class ProcessLobbiesCommandHandler : Contracts.ICommandHandler<ProcessLobbiesCommand>
    {
        private readonly IClusterClient _clusterClient;
        private readonly ILobbyRepository _lobbyRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IPlayerRepository _playerRepository;

        public ProcessLobbiesCommandHandler(
            IClusterClient clusterClient,
            ILobbyRepository lobbyRepository,
            IMatchRepository matchRepository,
            IPlayerRepository playerRepository)
        {
            _clusterClient = clusterClient;
            _lobbyRepository = lobbyRepository;
            _matchRepository = matchRepository;
            _playerRepository = playerRepository;
        }

        public async Task<Unit> Handle(ProcessLobbiesCommand request, CancellationToken cancellationToken)
        {
            var lobbies = (await GetLobbies()).ToList();
            var players = (await GetPlayers(lobbies)).ToDictionary(p => p.Id);

            var manager = await MatchmakingManager.CreateAsync(_matchRepository, lobbies, players);

            var response = await _clusterClient.GetAvailableServersCountAsync();

            var matches = manager.CreateMatches().Take(response.Count).ToList();
            
            foreach (var match in matches)
            {
                var lobbiesToClose = lobbies.Where(l => match.Lobbies.Any(entry => entry.LobbyId == l.Id));

                foreach (var lobbyToClose in lobbiesToClose)
                {
                    lobbyToClose.Close();
                }

                _matchRepository.Add(match);
            }

            if (matches.Count > 0)
            {
                await _clusterClient.CreateServersAsync(new CreateServersRequest(matches.Select(m => m.Id.Value)));
            }

            return Unit.Value;
        }

        private Task<IEnumerable<Lobby>> GetLobbies() => _lobbyRepository.BrowseByStatusAsync(LobbyStatus.Searching);

        private Task<IEnumerable<Player>> GetPlayers(IEnumerable<Lobby> lobbies)
            => _playerRepository.BrowseByIdAsync(lobbies.SelectMany(l => l.Members.Select(m => m.PlayerId)));
    }
}