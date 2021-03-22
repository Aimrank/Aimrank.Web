using Aimrank.Modules.Matches.Domain.Matches;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Modules.Matches.Application.CSGO.Commands.StartMatch
{
    internal class StartMatchCommandHandler : IServerEventCommandHandler<StartMatchCommand>
    {
        private readonly IMatchRepository _matchRepository;

        public StartMatchCommandHandler(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public async Task<Unit> Handle(StartMatchCommand request, CancellationToken cancellationToken)
        {
            var match = await _matchRepository.GetByIdAsync(new MatchId(request.MatchId));
            
            match.SetStarted();
            
            return Unit.Value;
        }
    }
}