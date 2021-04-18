using Aimrank.Web.Modules.Cluster.Application.Contracts;
using Aimrank.Web.Modules.Cluster.Application.Entities;
using Aimrank.Web.Modules.Cluster.Application.Exceptions;
using Aimrank.Web.Modules.Cluster.Application.Repositories;
using MediatR;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Cluster.Application.Commands.CreateServers
{
    internal class CreateServersCommandHandler : ICommandHandler<CreateServersCommand>
    {
        private readonly ISteamTokenRepository _steamTokenRepository;
        private readonly IServerRepository _serverRepository;
        private readonly IPodRepository _podRepository;

        public CreateServersCommandHandler(
            ISteamTokenRepository steamTokenRepository,
            IServerRepository serverRepository,
            IPodRepository podRepository)
        {
            _steamTokenRepository = steamTokenRepository;
            _serverRepository = serverRepository;
            _podRepository = podRepository;
        }

        public async Task<Unit> Handle(CreateServersCommand request, CancellationToken cancellationToken)
        {
            var matches = request.MatchIds.ToList();
            
            var tokens = (await _steamTokenRepository.BrowseUnusedAsync(matches.Count)).ToList();
            if (tokens.Count != matches.Count)
            {
                throw new ClusterException("Not enough steam tokens.");
            }

            var entries = (await _podRepository.BrowseAsync())
                .Select(p => new PodDto
                {
                    Pod = p,
                    AvailableServers = p.MaxServers - p.Servers.Count
                })
                .ToList();

            for (var i = 0; i < matches.Count; i++)
            {
                var entry = entries.FirstOrDefault(e => e.AvailableServers > 0);
                if (entry is null)
                {
                    throw new ClusterException("No server available.");
                }

                var server = new Server
                {
                    Pod = entry.Pod,
                    MatchId = matches[i],
                    SteamToken = tokens[i]
                };

                entry.AvailableServers--;

                _serverRepository.Add(server);
            }
            
            return Unit.Value;
        }

        private class PodDto
        {
            public Pod Pod { get; set; }
            public int AvailableServers { get; set; }
        }
    }
}