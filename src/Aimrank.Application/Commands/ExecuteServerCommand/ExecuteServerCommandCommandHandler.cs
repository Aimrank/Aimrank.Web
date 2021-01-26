using Aimrank.Application.CSGO;
using Aimrank.Application.Contracts;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Commands.ExecuteServerCommand
{
    public class ExecuteServerCommandCommandHandler : ICommandHandler<ExecuteServerCommandCommand>
    {
        private readonly IServerProcessManager _serverProcessManager;

        public ExecuteServerCommandCommandHandler(IServerProcessManager serverProcessManager)
        {
            _serverProcessManager = serverProcessManager;
        }

        public async Task<Unit> Handle(ExecuteServerCommandCommand request, CancellationToken cancellationToken)
        {
            await _serverProcessManager.ExecuteCommandAsync(request.ServerId, request.Command);
            
            return Unit.Value;
        }
    }
}