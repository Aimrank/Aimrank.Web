using Aimrank.Application.CSGO;
using Aimrank.Application.Contracts;
using Aimrank.Common.Application.Events;
using Aimrank.Domain.Lobbies;
using Aimrank.Domain.Matches;
using Aimrank.IntegrationEvents.Matches;
using MediatR;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Commands.Matches.TimeoutReadyMatch
{
    public class TimeoutReadyMatchCommandHandler : ICommandHandler<TimeoutReadyMatchCommand>
    {
        private readonly IServerProcessManager _serverProcessManager;
        private readonly ILobbyRepository _lobbyRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IEventDispatcher _eventDispatcher;

        public TimeoutReadyMatchCommandHandler(
            IServerProcessManager serverProcessManager,
            ILobbyRepository lobbyRepository,
            IMatchRepository matchRepository,
            IEventDispatcher eventDispatcher)
        {
            _serverProcessManager = serverProcessManager;
            _lobbyRepository = lobbyRepository;
            _matchRepository = matchRepository;
            _eventDispatcher = eventDispatcher;
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

            foreach (var lobby in lobbies)
            {
                lobby.RestoreSearching();
            }
            
            _lobbyRepository.UpdateRange(lobbies);
            _matchRepository.Delete(match);

            _eventDispatcher.Dispatch(new MatchTimedOutEvent(match.Id, match.Lobbies.Select(l => l.LobbyId.Value)));
            
            return Unit.Value;
        }
    }
}