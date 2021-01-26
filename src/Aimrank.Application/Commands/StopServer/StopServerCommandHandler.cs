using Aimrank.Application.CSGO;
using Aimrank.Application.Contracts;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Commands.StopServer
{
    public class StopServerCommandHandler : ICommandHandler<StopServerCommand>
    {
        private readonly IServerProcessManager _serverProcessManager;

        public StopServerCommandHandler(IServerProcessManager serverProcessManager)
        {
            _serverProcessManager = serverProcessManager;
        }

        public async Task<Unit> Handle(StopServerCommand request, CancellationToken cancellationToken)
        {
            await _serverProcessManager.StopServerAsync(request.ServerId);
            
            return Unit.Value;
        }
    }
}