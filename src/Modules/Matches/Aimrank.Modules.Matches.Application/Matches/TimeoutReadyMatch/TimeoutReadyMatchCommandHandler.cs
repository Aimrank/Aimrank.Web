using Aimrank.Common.Application.Events;
using Aimrank.Modules.Matches.Application.CSGO;
using Aimrank.Modules.Matches.Application.Contracts;
using Aimrank.Modules.Matches.Domain.Lobbies;
using Aimrank.Modules.Matches.Domain.Matches;
using Aimrank.Modules.Matches.IntegrationEvents.Matches;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Modules.Matches.Application.Matches.TimeoutReadyMatch
{
    internal class TimeoutReadyMatchCommandHandler : ICommandHandler<TimeoutReadyMatchCommand>
    {
        private readonly IServerProcessManager _serverProcessManager;
        private readonly ILobbyRepository _lobbyRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IMatchService _matchService;

        public TimeoutReadyMatchCommandHandler(
            IServerProcessManager serverProcessManager,
            ILobbyRepository lobbyRepository,
            IMatchRepository matchRepository,
            IEventDispatcher eventDispatcher,
            IMatchService matchService)
        {
            _serverProcessManager = serverProcessManager;
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
            
            _serverProcessManager.StopServer(match.Id);
            _serverProcessManager.DeleteReservation(match.Id);
            
            var lobbies = await _lobbyRepository.BrowseByIdAsync(match.Lobbies.Select(l => l.LobbyId));

            var notAcceptedUsers = new HashSet<Guid>(await _matchService.GetNotAcceptedUsersAsync(match.Id));

            foreach (var lobby in lobbies)
            {
                if (lobby.Members.Any(m => notAcceptedUsers.Contains(m.UserId)))
                {
                    lobby.Open();
                }
                else
                {
                    lobby.RestoreSearching();
                }
            }
            
            _lobbyRepository.UpdateRange(lobbies);
            _matchRepository.Delete(match);

            _eventDispatcher.Dispatch(new MatchTimedOutEvent(match.Id, match.Lobbies.Select(l => l.LobbyId.Value)));
            
            return Unit.Value;
        }
    }
}