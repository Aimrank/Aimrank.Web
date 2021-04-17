using Aimrank.Web.Modules.Cluster.Application.Contracts;
using Aimrank.Web.Modules.Cluster.Application.Entities;
using Aimrank.Web.Modules.Cluster.Application.Repositories;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Cluster.Application.Commands.CreatePod
{
    internal class CreatePodCommandHandler : ICommandHandler<CreatePodCommand>
    {
        private readonly IPodRepository _podRepository;

        public CreatePodCommandHandler(IPodRepository podRepository)
        {
            _podRepository = podRepository;
        }

        public async Task<Unit> Handle(CreatePodCommand request, CancellationToken cancellationToken)
        {
            var pod = await _podRepository.GetByIpAddressAsync(request.IpAddress);
            if (pod is not null)
            {
                return Unit.Value;
            }

            pod = new Pod
            {
                IpAddress = request.IpAddress,
                MaxServers = request.MaxServers
            };

            _podRepository.Add(pod);
            
            return Unit.Value;
        }
    }
}