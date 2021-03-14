using Aimrank.Modules.Matches.Domain.Lobbies;
using Aimrank.Modules.Matches.Domain.Matches;
using MediatR;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Modules.Matches.Application.CSGO.Commands.FinishMatch
{
    internal class FinishMatchCommandHandler : IServerEventCommandHandler<FinishMatchCommand>
    {
        private readonly IServerProcessManager _serverProcessManager;
        private readonly IMatchRepository _matchRepository;
        private readonly ILobbyRepository _lobbyRepository;

        public FinishMatchCommandHandler(
            IServerProcessManager serverProcessManager,
            IMatchRepository matchRepository,
            ILobbyRepository lobbyRepository)
        {
            _serverProcessManager = serverProcessManager;
            _matchRepository = matchRepository;
            _lobbyRepository = lobbyRepository;
        }

        public async Task<Unit> Handle(FinishMatchCommand request, CancellationToken cancellationToken)
        {
            _serverProcessManager.StopServer(request.MatchId);
            
            var match = await _matchRepository.GetByIdAsync(new MatchId(request.MatchId));
            var lobbies = await _lobbyRepository.BrowseByIdAsync(match.Lobbies.Select(l => l.LobbyId));

            match.UpdateScore(
                (MatchWinner) request.Winner,
                request.TeamTerrorists.Score,
                request.TeamCounterTerrorists.Score);
            
            foreach (var client in request.TeamTerrorists.Clients.Concat(request.TeamCounterTerrorists.Clients))
            {
                match.UpdatePlayerStats(client.SteamId, new MatchPlayerStats(
                    client.Kills,
                    client.Assists,
                    client.Deaths,
                    client.Hs));
            }
            
            match.Finish();

            foreach (var lobby in lobbies)
            {
                lobby.Open();
            }
            
            _matchRepository.Update(match);
            _lobbyRepository.UpdateRange(lobbies);
            
            return Unit.Value;
        }
    }
}