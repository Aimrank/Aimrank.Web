using Aimrank.Web.Modules.Matches.Domain.Lobbies;
using Aimrank.Web.Modules.Matches.Domain.Matches;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.Modules.Matches.Application.Matches.TimeoutReadyMatch
{
    internal class TimeoutReadyMatchCommandHandler : Contracts.ICommandHandler<TimeoutReadyMatchCommand>
    {
        private readonly ILobbyRepository _lobbyRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IMatchService _matchService;

        public TimeoutReadyMatchCommandHandler(
            ILobbyRepository lobbyRepository,
            IMatchRepository matchRepository,
            IMatchService matchService)
        {
            _lobbyRepository = lobbyRepository;
            _matchRepository = matchRepository;
            _matchService = matchService;
        }

        public async Task<Unit> Handle(TimeoutReadyMatchCommand request, CancellationToken cancellationToken)
        {
            var match = await _matchRepository.GetByIdAsync(new MatchId(request.MatchId));

            if (match.Status != MatchStatus.Ready)
            {
                return Unit.Value;
            }
            
            var lobbies = await _lobbyRepository.BrowseByIdAsync(match.Lobbies.Select(l => l.LobbyId));

            var notAcceptedPlayers = new HashSet<Guid>(await _matchService.GetNotAcceptedPlayersAsync(match.Id));

            foreach (var lobby in lobbies)
            {
                if (lobby.Members.Any(m => notAcceptedPlayers.Contains(m.PlayerId)))
                {
                    lobby.Open();
                }
                else
                {
                    lobby.RestoreSearching();
                }
            }

            match.Timeout();
            
            _matchRepository.Delete(match);
            
            return Unit.Value;
        }
    }
}