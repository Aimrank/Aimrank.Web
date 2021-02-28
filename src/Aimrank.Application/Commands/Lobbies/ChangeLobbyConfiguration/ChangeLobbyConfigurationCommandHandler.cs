using Aimrank.Application.Contracts;
using Aimrank.Common.Application;
using Aimrank.Domain.Lobbies;
using Aimrank.Domain.Users;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Aimrank.Domain.Matches;

namespace Aimrank.Application.Commands.Lobbies.ChangeLobbyConfiguration
{
    internal class ChangeLobbyConfigurationCommandHandler : ICommandHandler<ChangeLobbyConfigurationCommand>
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly ILobbyRepository _lobbyRepository;

        public ChangeLobbyConfigurationCommandHandler(
            IExecutionContextAccessor executionContextAccessor,
            ILobbyRepository lobbyRepository)
        {
            _executionContextAccessor = executionContextAccessor;
            _lobbyRepository = lobbyRepository;
        }

        public async Task<Unit> Handle(ChangeLobbyConfigurationCommand request, CancellationToken cancellationToken)
        {
            var lobby = await _lobbyRepository.GetByIdAsync(new LobbyId(request.LobbyId));
            var userId = new UserId(_executionContextAccessor.UserId);
            
            lobby.ChangeConfiguration(userId, new LobbyConfiguration(
                request.Name,
                request.Map,
                (MatchMode) request.Mode));
            
            _lobbyRepository.Update(lobby);
            
            return Unit.Value;
        }
    }
}