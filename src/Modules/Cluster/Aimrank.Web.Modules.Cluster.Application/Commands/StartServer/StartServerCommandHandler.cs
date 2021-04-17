using Aimrank.Web.Modules.Cluster.Application.Contracts;
using Aimrank.Web.Modules.Cluster.Application.Repositories;
using Aimrank.Web.Modules.Cluster.Application.Services;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Cluster.Application.Commands.StartServer
{
    internal class StartServerCommandHandler : ICommandHandler<StartServerCommand, string>
    {
        private readonly IServerRepository _serverRepository;
        private readonly IPodClient _podClient;

        public StartServerCommandHandler(IServerRepository serverRepository, IPodClient podClient)
        {
            _serverRepository = serverRepository;
            _podClient = podClient;
        }

        public async Task<string> Handle(StartServerCommand request, CancellationToken cancellationToken)
        {
            var server = await _serverRepository.GetByMatchIdAsync(request.MatchId);

            server.IsAccepted = true;

            return await _podClient.StartServerAsync(server, request.Map, request.Whitelist);
        }
    }
}