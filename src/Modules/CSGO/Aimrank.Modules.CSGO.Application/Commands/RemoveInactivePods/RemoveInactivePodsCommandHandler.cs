using Aimrank.Common.Application.Events;
using Aimrank.Modules.CSGO.Application.Contracts;
using Aimrank.Modules.CSGO.Application.Repositories;
using Aimrank.Modules.CSGO.Application.Services;
using Aimrank.Modules.CSGO.IntegrationEvents.Servers;
using MediatR;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Modules.CSGO.Application.Commands.RemoveInactivePods
{
    internal class RemoveInactivePodsCommandHandler : ICommandHandler<RemoveInactivePodsCommand>
    {
        private readonly IPodService _podService;
        private readonly IPodRepository _podRepository;
        private readonly IServerRepository _serverRepository;
        private readonly IEventDispatcher _eventDispatcher;

        public RemoveInactivePodsCommandHandler(
            IPodService podService,
            IPodRepository podRepository,
            IServerRepository serverRepository,
            IEventDispatcher eventDispatcher)
        {
            _podService = podService;
            _podRepository = podRepository;
            _serverRepository = serverRepository;
            _eventDispatcher = eventDispatcher;
        }

        public async Task<Unit> Handle(RemoveInactivePodsCommand request, CancellationToken cancellationToken)
        {
            var inactivePods = (await _podService.GetInactivePodsAsync()).ToList();
            var inactivePodsIp = inactivePods.Select(p => p.IpAddress);

            var inactiveServers = (await _serverRepository.BrowseByIpAddressesAsync(inactivePodsIp)).ToList();
            
            _serverRepository.DeleteRange(inactiveServers);

            _podRepository.DeleteRange(inactivePods);

            await _eventDispatcher.DispatchAsync(new ServersDeletedEvent(inactiveServers.Select(s => s.MatchId)));

            return Unit.Value;
        }
    }
}