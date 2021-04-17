using Aimrank.Web.Modules.Matches.Application.Contracts;
using Aimrank.Web.Modules.Matches.Domain.Lobbies;
using Aimrank.Web.Modules.Matches.Domain.Matches;
using MediatR;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Application.Matches.FinishMatch
{
    internal class FinishMatchCommandHandler : ICommandHandler<FinishMatchCommand>
    {
        private readonly IMatchRepository _matchRepository;
        private readonly ILobbyRepository _lobbyRepository;

        public FinishMatchCommandHandler(IMatchRepository matchRepository, ILobbyRepository lobbyRepository)
        {
            _matchRepository = matchRepository;
            _lobbyRepository = lobbyRepository;
        }

        public async Task<Unit> Handle(FinishMatchCommand request, CancellationToken cancellationToken)
        {
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
            
            return Unit.Value;
        }
    }
}