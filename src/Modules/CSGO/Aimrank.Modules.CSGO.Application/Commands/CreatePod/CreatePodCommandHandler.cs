using Aimrank.Modules.CSGO.Application.Contracts;
using Aimrank.Modules.CSGO.Application.Entities;
using Aimrank.Modules.CSGO.Application.Repositories;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Modules.CSGO.Application.Commands.CreatePod
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
                throw new Exception("Pod with this ip address already exists");
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