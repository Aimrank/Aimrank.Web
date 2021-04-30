using Aimrank.Web.Modules.Matches.Application.Contracts;
using Aimrank.Web.Modules.Matches.Domain.Lobbies;
using Aimrank.Web.Modules.Matches.Domain.Matches;
using MediatR;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Application.Matches.CancelMatches
{
    internal class CancelMatchesCommandHandler : ICommandHandler<CancelMatchesCommand>
    {
        private readonly IMatchRepository _matchRepository;
        private readonly ILobbyRepository _lobbyRepository;

        public CancelMatchesCommandHandler(IMatchRepository matchRepository, ILobbyRepository lobbyRepository)
        {
            _matchRepository = matchRepository;
            _lobbyRepository = lobbyRepository;
        }

        public async Task<Unit> Handle(CancelMatchesCommand request, CancellationToken cancellationToken)
        {
            var matches = await _matchRepository.BrowseByIdAsync(request.Ids.Select(id => new MatchId(id)));
            var lobbies = await _lobbyRepository.BrowseByIdAsync(matches.SelectMany(m => m.Lobbies).Select(l => l.LobbyId));

            foreach (var match in matches)
            {
                match.Cancel();
                _matchRepository.Delete(match);
            }

            foreach (var lobby in lobbies)
            {
                lobby.Open();
            }
            
            return Unit.Value;
        }
    }
}