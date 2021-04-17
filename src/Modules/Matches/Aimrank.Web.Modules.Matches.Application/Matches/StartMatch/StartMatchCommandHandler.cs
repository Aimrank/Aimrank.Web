using Aimrank.Web.Modules.Matches.Application.Contracts;
using Aimrank.Web.Modules.Matches.Domain.Matches;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Application.Matches.StartMatch
{
    internal class StartMatchCommandHandler : ICommandHandler<StartMatchCommand>
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