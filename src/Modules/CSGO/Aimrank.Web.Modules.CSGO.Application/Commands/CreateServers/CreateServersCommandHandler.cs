using Aimrank.Web.Modules.CSGO.Application.Contracts;
using Aimrank.Web.Modules.CSGO.Application.Entities;
using Aimrank.Web.Modules.CSGO.Application.Exceptions;
using Aimrank.Web.Modules.CSGO.Application.Repositories;
using MediatR;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.CSGO.Application.Commands.CreateServers
{
    internal class CreateServersCommandHandler : ICommandHandler<CreateServersCommand>
    {
        private readonly ISteamTokenRepository _steamTokenRepository;
        private readonly IPodRepository _podRepository;

        public CreateServersCommandHandler(ISteamTokenRepository steamTokenRepository, IPodRepository podRepository)
        {
            _steamTokenRepository = steamTokenRepository;
            _podRepository = podRepository;
        }

        public async Task<Unit> Handle(CreateServersCommand request, CancellationToken cancellationToken)
        {
            var matches = request.MatchIds.ToList();
            
            var tokens = (await _steamTokenRepository.BrowseUnusedAsync(matches.Count)).ToList();
            if (tokens.Count != matches.Count)
            {
                throw new CSGOException("Not enough steam tokens.");
            }

            var pods = (await _podRepository.BrowseAsync()).ToList();

            for (var i = 0; i < matches.Count; i++)
            {
                var pod = pods.FirstOrDefault(p => p.Servers.Count < p.MaxServers);
                if (pod is null)
                {
                    throw new CSGOException("No server available.");
                }
                
                pod.Servers.Add(new Server
                {
                    MatchId = matches[i],
                    SteamToken = tokens[i]
                });
            }
            
            return Unit.Value;
        }
    }
}