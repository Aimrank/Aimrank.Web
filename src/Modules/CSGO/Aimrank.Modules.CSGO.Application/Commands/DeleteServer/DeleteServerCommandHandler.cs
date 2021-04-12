using Aimrank.Modules.CSGO.Application.Contracts;
using Aimrank.Modules.CSGO.Application.Repositories;
using Aimrank.Modules.CSGO.Application.Services;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Modules.CSGO.Application.Commands.DeleteServer
{
    internal class DeleteServerCommandHandler : ICommandHandler<DeleteServerCommand>
    {
        private readonly IServerRepository _serverRepository;
        private readonly IPodClient _podClient;

        public DeleteServerCommandHandler(IServerRepository serverRepository, IPodClient podClient)
        {
            _serverRepository = serverRepository;
            _podClient = podClient;
        }

        public async Task<Unit> Handle(DeleteServerCommand request, CancellationToken cancellationToken)
        {
            var server = await _serverRepository.GetByMatchIdAsync(request.MatchId);
            if (server is null)
            {
                throw new Exception($"Server for match with id {request.MatchId} does not exist.");
            }

            if (server.IsAccepted)
            {
                await _podClient.StopServerAsync(server);
            }
            
            _serverRepository.Delete(server);

            return Unit.Value;
        }
    }
}