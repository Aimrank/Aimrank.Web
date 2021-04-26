using Aimrank.Web.Modules.Cluster.Application.Clients;
using Aimrank.Web.Modules.Cluster.Application.Contracts;
using Aimrank.Web.Modules.Cluster.Application.Repositories;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Cluster.Application.Commands.DeleteAndStopServer
{
    internal class DeleteAndStopServerCommandHandler : ICommandHandler<DeleteAndStopServerCommand>
    {
        private readonly IServerRepository _serverRepository;
        private readonly IPodClient _podClient;

        public DeleteAndStopServerCommandHandler(IServerRepository serverRepository, IPodClient podClient)
        {
            _serverRepository = serverRepository;
            _podClient = podClient;
        }

        public async Task<Unit> Handle(DeleteAndStopServerCommand request, CancellationToken cancellationToken)
        {
            var server = await _serverRepository.GetByMatchIdAsync(request.MatchId);

            if (server.IsAccepted)
            {
                await _podClient.StopServerAsync(server);
            }
            
            _serverRepository.Delete(server);

            return Unit.Value;
        }
    }
}