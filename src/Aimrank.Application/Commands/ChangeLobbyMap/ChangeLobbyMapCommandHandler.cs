using Aimrank.Application.Contracts;
using Aimrank.Domain.Lobbies;
using Aimrank.Domain.Users;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Commands.ChangeLobbyMap
{
    public class ChangeLobbyMapCommandHandler : ICommandHandler<ChangeLobbyMapCommand>
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly ILobbyRepository _lobbyRepository;

        public ChangeLobbyMapCommandHandler(
            IExecutionContextAccessor executionContextAccessor,
            ILobbyRepository lobbyRepository)
        {
            _executionContextAccessor = executionContextAccessor;
            _lobbyRepository = lobbyRepository;
        }

        public async Task<Unit> Handle(ChangeLobbyMapCommand request, CancellationToken cancellationToken)
        {
            var userId = new UserId(_executionContextAccessor.UserId);
            
            var lobbyId = new LobbyId(request.Id);
            var lobby = await _lobbyRepository.GetByIdAsync(lobbyId);
            
            lobby.ChangeMap(userId, request.Name);
            
            _lobbyRepository.Update(lobby);
            
            return Unit.Value;
        }
    }
}