using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.Matches.Application.Contracts;
using Aimrank.Web.Modules.Matches.Domain.Lobbies;
using Aimrank.Web.Modules.Matches.Domain.Matches;
using Aimrank.Web.Modules.Matches.IntegrationEvents.Matches;
using MediatR;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Application.Matches.CancelMatch
{
    internal class CancelMatchCommandHandler : ICommandHandler<CancelMatchCommand>
    {
        private readonly IMatchRepository _matchRepository;
        private readonly ILobbyRepository _lobbyRepository;
        private readonly IEventDispatcher _eventDispatcher;

        public CancelMatchCommandHandler(
            IMatchRepository matchRepository,
            ILobbyRepository lobbyRepository,
            IEventDispatcher eventDispatcher)
        {
            _matchRepository = matchRepository;
            _lobbyRepository = lobbyRepository;
            _eventDispatcher = eventDispatcher;
        }

        public async Task<Unit> Handle(CancelMatchCommand request, CancellationToken cancellationToken)
        {
            var match = await _matchRepository.GetByIdAsync(new MatchId(request.MatchId));
            var lobbies = await _lobbyRepository.BrowseByIdAsync(match.Lobbies.Select(l => l.LobbyId));
            
            foreach (var lobby in lobbies)
            {
                lobby.Open();
            }
            
            _matchRepository.Delete(match);
            
            await _eventDispatcher.DispatchAsync(new MatchCanceledEvent(match.Id, lobbies.Select(l => l.Id.Value)));

            return Unit.Value;
        }
    }
}