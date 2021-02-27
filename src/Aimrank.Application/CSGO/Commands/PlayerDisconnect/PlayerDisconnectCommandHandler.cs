using Aimrank.Domain.Matches;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.CSGO.Commands.PlayerDisconnect
{
    public class PlayerDisconnectCommandHandler : IServerEventCommandHandler<PlayerDisconnectCommand>
    {
        private readonly IMatchRepository _matchRepository;

        public PlayerDisconnectCommandHandler(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public async Task<Unit> Handle(PlayerDisconnectCommand request, CancellationToken cancellationToken)
        {
            var match = await _matchRepository.GetByIdAsync(new MatchId(request.MatchId));

            match.MarkPlayerAsLeaver(request.SteamId);
            
            _matchRepository.Update(match);
            
            return Unit.Value;
        }
    }
}