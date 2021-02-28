using Aimrank.Application.Contracts;
using Aimrank.Application.Services;
using Aimrank.Common.Application;
using Aimrank.Domain.Matches;
using Aimrank.Domain.Users;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Commands.Matches.AcceptMatch
{
    internal class AcceptMatchCommandHandler : ICommandHandler<AcceptMatchCommand>
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly IMatchRepository _matchRepository;
        private readonly IMatchService _matchService;

        public AcceptMatchCommandHandler(
            IExecutionContextAccessor executionContextAccessor,
            IMatchRepository matchRepository,
            IMatchService matchService)
        {
            _executionContextAccessor = executionContextAccessor;
            _matchRepository = matchRepository;
            _matchService = matchService;
        }

        public async Task<Unit> Handle(AcceptMatchCommand request, CancellationToken cancellationToken)
        {
            var match = await _matchRepository.GetByIdAsync(new MatchId(request.MatchId));
            
            await _matchService.AcceptMatchAsync(match, new UserId(_executionContextAccessor.UserId));
            
            return Unit.Value;
        }
    }
}