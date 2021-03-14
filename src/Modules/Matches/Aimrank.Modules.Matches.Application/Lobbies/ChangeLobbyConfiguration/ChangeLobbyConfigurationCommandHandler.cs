using Aimrank.Common.Application;
using Aimrank.Modules.Matches.Application.Contracts;
using Aimrank.Modules.Matches.Domain.Lobbies;
using Aimrank.Modules.Matches.Domain.Matches;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Modules.Matches.Application.Lobbies.ChangeLobbyConfiguration
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
            
            lobby.ChangeConfiguration(_executionContextAccessor.UserId, new LobbyConfiguration(
                request.Name,
                request.Map,
                (MatchMode) request.Mode));
            
            _lobbyRepository.Update(lobby);
            
            return Unit.Value;
        }
    }
}