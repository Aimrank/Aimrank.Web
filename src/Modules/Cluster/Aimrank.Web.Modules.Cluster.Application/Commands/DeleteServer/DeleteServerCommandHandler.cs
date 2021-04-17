using Aimrank.Web.Modules.Cluster.Application.Contracts;
using Aimrank.Web.Modules.Cluster.Application.Repositories;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Cluster.Application.Commands.DeleteServer
{
    internal class DeleteServerCommandHandler : ICommandHandler<DeleteServerCommand>
    {
        private readonly IServerRepository _serverRepository;

        public DeleteServerCommandHandler(IServerRepository serverRepository)
        {
            _serverRepository = serverRepository;
        }

        public async Task<Unit> Handle(DeleteServerCommand request, CancellationToken cancellationToken)
        {
            var server = await _serverRepository.GetByMatchIdAsync(request.MatchId);
            
            _serverRepository.Delete(server);
            
            return Unit.Value;
        }
    }
}