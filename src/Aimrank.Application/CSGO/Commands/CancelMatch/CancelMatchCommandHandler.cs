using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Application.CSGO.Commands.CancelMatch
{
    public class CancelMatchCommandHandler : IServerEventCommandHandler<CancelMatchCommand>
    {
        public Task<Unit> Handle(CancelMatchCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Canceling match: {request.MatchId}");

            return Task.FromResult(Unit.Value);
        }
    }
}