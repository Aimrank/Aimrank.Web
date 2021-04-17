using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.Cluster.Application.Commands.DeleteAndStopServer;
using Aimrank.Web.Modules.Cluster.Application.Contracts;
using Aimrank.Web.Modules.Matches.Domain.Lobbies;
using Aimrank.Web.Modules.Matches.Domain.Matches;
using Aimrank.Web.Modules.Matches.IntegrationEvents.Matches;
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
        private readonly IClusterModule _clusterModule;
        private readonly ILobbyRepository _lobbyRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IMatchService _matchService;

        public TimeoutReadyMatchCommandHandler(
            IClusterModule clusterModule,
            ILobbyRepository lobbyRepository,
            IMatchRepository matchRepository,
            IEventDispatcher eventDispatcher,
            IMatchService matchService)
        {
            _clusterModule = clusterModule;
            _lobbyRepository = lobbyRepository;
            _matchRepository = matchRepository;
            _eventDispatcher = eventDispatcher;
            _matchService = matchService;
        }

        public async Task<Unit> Handle(TimeoutReadyMatchCommand request, CancellationToken cancellationToken)
        {
            var match = await _matchRepository.GetByIdAsync(new MatchId(request.MatchId));

            if (match.Status != MatchStatus.Ready)
            {
                return Unit.Value;
            }

            await _clusterModule.ExecuteCommandAsync(new DeleteAndStopServerCommand(match.Id));
            
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
            
            _matchRepository.Delete(match);

            await _eventDispatcher.DispatchAsync(new MatchTimedOutEvent(match.Id, match.Lobbies.Select(l => l.LobbyId.Value)));
            
            return Unit.Value;
        }
    }
}