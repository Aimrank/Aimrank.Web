using Aimrank.Modules.CSGO.Application.Contracts;
using Aimrank.Modules.CSGO.Application.Repositories;
using Aimrank.Modules.CSGO.Application.Services;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Modules.CSGO.Application.Commands.StartServer
{
    internal class StartServerCommandHandler : ICommandHandler<StartServerCommand>
    {
        private readonly IServerRepository _serverRepository;
        private readonly IPodClient _podClient;

        public StartServerCommandHandler(IServerRepository serverRepository, IPodClient podClient)
        {
            _serverRepository = serverRepository;
            _podClient = podClient;
        }

        public async Task<Unit> Handle(StartServerCommand request, CancellationToken cancellationToken)
        {
            var server = await _serverRepository.GetByMatchIdAsync(request.MatchId);

            server.IsAccepted = true;

            await _podClient.StartServerAsync(server, request.Map, request.Whitelist);
            
            return Unit.Value;
        }
    }
}