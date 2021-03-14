using Aimrank.Modules.Matches.Application.Contracts;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Modules.Matches.Application.CSGO.Commands.ProcessServerEvent
{
    internal class ProcessServerEventCommandHandler : ICommandHandler<ProcessServerEventCommand>
    {
        private readonly IServerEventMapper _serverEventMapper;
        private readonly IMediator _mediator;

        public ProcessServerEventCommandHandler(IServerEventMapper serverEventMapper, IMediator mediator)
        {
            _serverEventMapper = serverEventMapper;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(ProcessServerEventCommand request, CancellationToken cancellationToken)
        {
            var command = _serverEventMapper.Map(request.MatchId, request.Name, request.Data);
            if (command is null)
            {
                return Unit.Value;
            }

            await _mediator.Send(command, cancellationToken);

            return Unit.Value;
        }
    }
}