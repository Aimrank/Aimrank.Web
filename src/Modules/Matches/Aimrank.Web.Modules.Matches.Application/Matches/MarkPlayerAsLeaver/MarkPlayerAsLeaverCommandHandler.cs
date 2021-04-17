using Aimrank.Web.Modules.Matches.Application.Contracts;
using Aimrank.Web.Modules.Matches.Domain.Matches;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Application.Matches.MarkPlayerAsLeaver
{
    internal class MarkPlayerAsLeaverCommandHandler : ICommandHandler<MarkPlayerAsLeaverCommand>
    {
        private readonly IMatchRepository _matchRepository;

        public MarkPlayerAsLeaverCommandHandler(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public async Task<Unit> Handle(MarkPlayerAsLeaverCommand request, CancellationToken cancellationToken)
        {
            var match = await _matchRepository.GetByIdAsync(new MatchId(request.MatchId));

            match.MarkPlayerAsLeaver(request.SteamId);
            
            return Unit.Value;
        }
    }
}