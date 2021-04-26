using Aimrank.Web.Modules.Cluster.Application.Contracts;
using Aimrank.Web.Modules.Cluster.Application.Repositories;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Aimrank.Web.Modules.Cluster.Application.Exceptions;

namespace Aimrank.Web.Modules.Cluster.Application.Commands.DeleteSteamToken
{
    internal class DeleteSteamTokenCommandHandler : ICommandHandler<DeleteSteamTokenCommand>
    {
        private readonly ISteamTokenRepository _steamTokenRepository;

        public DeleteSteamTokenCommandHandler(ISteamTokenRepository steamTokenRepository)
        {
            _steamTokenRepository = steamTokenRepository;
        }

        public async Task<Unit> Handle(DeleteSteamTokenCommand request, CancellationToken cancellationToken)
        {
            var steamToken = await _steamTokenRepository.GetAsync(request.Token);

            if (steamToken.Server is not null)
            {
                throw new ClusterException(
                    $"Cannot delete steam key \"{request.Token}\" because it's currently used by server \"{steamToken.Server.MatchId}\".");
            }

            _steamTokenRepository.Delete(steamToken);
            
            return Unit.Value;
        }
    }
}