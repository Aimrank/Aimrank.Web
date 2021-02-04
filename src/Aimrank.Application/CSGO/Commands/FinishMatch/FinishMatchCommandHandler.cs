using Aimrank.Domain.Lobbies;
using Aimrank.Domain.Matches;
using MediatR;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.CSGO.Commands.FinishMatch
{
    public class FinishMatchCommandHandler : IServerEventCommandHandler<FinishMatchCommand>
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
            await _serverProcessManager.StopServerAsync(request.ServerId);

            var match = await _matchRepository.GetByIdAsync(new MatchId(request.ServerId));
            var lobby = await _lobbyRepository.GetByMatchIdAsync(match.Id);

            foreach (var player in match.Players)
            {
                var client =
                    request.TeamTerrorists.Clients.FirstOrDefault(c => c.SteamId == player.SteamId) ??
                    request.TeamCounterTerrorists.Clients.FirstOrDefault(c => c.SteamId == player.SteamId);

                if (client is null)
                {
                    continue;
                }

                player.UpdateStats(client.Kills, client.Assists, client.Deaths, client.Score);
            }
            
            match.Finish(
                request.TeamTerrorists.Score,
                request.TeamCounterTerrorists.Score);
            
            lobby.Open();
            
            _matchRepository.Update(match);
            _lobbyRepository.Update(lobby);
            
            return Unit.Value;
        }
    }
}