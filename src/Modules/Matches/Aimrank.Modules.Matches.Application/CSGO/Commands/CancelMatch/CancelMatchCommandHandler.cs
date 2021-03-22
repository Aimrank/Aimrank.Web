using Aimrank.Common.Application.Events;
using Aimrank.Modules.Matches.Domain.Lobbies;
using Aimrank.Modules.Matches.Domain.Matches;
using Aimrank.Modules.Matches.IntegrationEvents.Matches;
using MediatR;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Modules.Matches.Application.CSGO.Commands.CancelMatch
{
    internal class CancelMatchCommandHandler : IServerEventCommandHandler<CancelMatchCommand>
    {
        private readonly IServerProcessManager _serverProcessManager;
        private readonly IMatchRepository _matchRepository;
        private readonly ILobbyRepository _lobbyRepository;
        private readonly IEventDispatcher _eventDispatcher;

        public CancelMatchCommandHandler(
            IServerProcessManager serverProcessManager,
            IMatchRepository matchRepository,
            ILobbyRepository lobbyRepository,
            IEventDispatcher eventDispatcher)
        {
            _serverProcessManager = serverProcessManager;
            _matchRepository = matchRepository;
            _lobbyRepository = lobbyRepository;
            _eventDispatcher = eventDispatcher;
        }

        public async Task<Unit> Handle(CancelMatchCommand request, CancellationToken cancellationToken)
        {
            _serverProcessManager.StopServer(request.MatchId);

            var match = await _matchRepository.GetByIdAsync(new MatchId(request.MatchId));
            var lobbies = await _lobbyRepository.BrowseByIdAsync(match.Lobbies.Select(l => l.LobbyId));
            
            foreach (var lobby in lobbies)
            {
                lobby.Open();
            }
            
            _matchRepository.Delete(match);
            
            _eventDispatcher.Dispatch(new MatchCanceledEvent(match.Id, lobbies.Select(l => l.Id.Value)));

            return Unit.Value;
        }
    }
}