using Aimrank.Application.CSGO;
using Aimrank.Application.Contracts;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Commands.StartServer
{
    public class StartServerCommandHandler : ICommandHandler<StartServerCommand>
    {
        private readonly IServerProcessManager _serverProcessManager;

        public StartServerCommandHandler(IServerProcessManager serverProcessManager)
        {
            _serverProcessManager = serverProcessManager;
        }

        public Task<Unit> Handle(StartServerCommand request, CancellationToken cancellationToken)
        {
            _serverProcessManager.StartServer(request.ServerId, request.Whitelist, request.Map);

            return Task.FromResult(Unit.Value);
        }
    }
}