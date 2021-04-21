using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.Cluster.Application.Contracts;
using Aimrank.Web.Modules.Cluster.Application.Repositories;
using Aimrank.Web.Modules.Cluster.Application.Services;
using Aimrank.Web.Modules.Cluster.IntegrationEvents.Servers;
using MediatR;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Cluster.Application.Commands.RemoveInactivePods
{
    internal class RemoveInactivePodsCommandHandler : ICommandHandler<RemoveInactivePodsCommand>
    {
        private readonly IEventBus _eventBus;
        private readonly IPodClient _podClient;
        private readonly IPodRepository _podRepository;
        private readonly IServerRepository _serverRepository;

        public RemoveInactivePodsCommandHandler(
            IEventBus eventBus,
            IPodClient podClient,
            IPodRepository podRepository,
            IServerRepository serverRepository)
        {
            _eventBus = eventBus;
            _podClient = podClient;
            _podRepository = podRepository;
            _serverRepository = serverRepository;
        }

        public async Task<Unit> Handle(RemoveInactivePodsCommand request, CancellationToken cancellationToken)
        {
            var inactivePods = (await _podClient.GetInactivePodsAsync()).ToList();
            var inactivePodsIp = inactivePods.Select(p => p.IpAddress);

            var inactiveServers = (await _serverRepository.BrowseByIpAddressesAsync(inactivePodsIp)).ToList();
            if (inactiveServers.Any())
            {
                _serverRepository.DeleteRange(inactiveServers);
                _podRepository.DeleteRange(inactivePods);

                await _eventBus.Publish(new ServersDeletedEvent(inactiveServers.Select(s => s.MatchId)));
            }

            return Unit.Value;
        }
    }
}