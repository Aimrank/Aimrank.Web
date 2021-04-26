using Aimrank.Web.Modules.Cluster.Application.Contracts;
using Aimrank.Web.Modules.Cluster.Application.Entities;
using Aimrank.Web.Modules.Cluster.Application.Exceptions;
using Aimrank.Web.Modules.Cluster.Application.Repositories;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Cluster.Application.Commands.AddSteamToken
{
    internal class AddSteamTokenCommandHandler : ICommandHandler<AddSteamTokenCommand>
    {
        private readonly ISteamTokenRepository _steamTokenRepository;

        public AddSteamTokenCommandHandler(ISteamTokenRepository steamTokenRepository)
        {
            _steamTokenRepository = steamTokenRepository;
        }

        public async Task<Unit> Handle(AddSteamTokenCommand request, CancellationToken cancellationToken)
        {
            var steamToken = await _steamTokenRepository.GetOptionalAsync(request.Token);
            if (steamToken is not null)
            {
                throw new ClusterException($"Steam token \"{request.Token}\" already exists.");
            }

            steamToken = new SteamToken {Token = request.Token};

            _steamTokenRepository.Add(steamToken);
            
            return Unit.Value;
        }
    }
}