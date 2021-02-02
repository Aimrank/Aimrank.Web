using Aimrank.Application.Contracts;
using Aimrank.Common.Application;
using Aimrank.Domain.Lobbies;
using Aimrank.Domain.Users;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Commands.CloseLobby
{
    public class CloseLobbyCommandHandler : ICommandHandler<CloseLobbyCommand>
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly ILobbyRepository _lobbyRepository;

        public CloseLobbyCommandHandler(
            IExecutionContextAccessor executionContextAccessor,
            ILobbyRepository lobbyRepository)
        {
            _executionContextAccessor = executionContextAccessor;
            _lobbyRepository = lobbyRepository;
        }

        public async Task<Unit> Handle(CloseLobbyCommand request, CancellationToken cancellationToken)
        {
            var userId = new UserId(_executionContextAccessor.UserId);
            var lobbyId = new LobbyId(request.Id);
            var lobby = await _lobbyRepository.GetByIdAsync(lobbyId);
            
            lobby.Close(userId);
            
            _lobbyRepository.Update(lobby);
            
            return Unit.Value;
        }
    }
}