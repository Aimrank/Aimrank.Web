using Aimrank.Application.Contracts;
using Aimrank.Common.Application;
using Aimrank.Domain.Lobbies;
using Aimrank.Domain.Users;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Commands.LeaveLobby
{
    public class LeaveLobbyCommandHandler : ICommandHandler<LeaveLobbyCommand>
    {
        private readonly ILobbyRepository _lobbyRepository;
        private readonly IExecutionContextAccessor _executionContextAccessor;

        public LeaveLobbyCommandHandler(
            ILobbyRepository lobbyRepository,
            IExecutionContextAccessor executionContextAccessor)
        {
            _lobbyRepository = lobbyRepository;
            _executionContextAccessor = executionContextAccessor;
        }

        public async Task<Unit> Handle(LeaveLobbyCommand request, CancellationToken cancellationToken)
        {
            var lobbyId = new LobbyId(request.Id);
            var userId = new UserId(_executionContextAccessor.UserId);
            var lobby = await _lobbyRepository.GetByIdAsync(lobbyId);
            
            lobby.Leave(userId);

            if (lobby.Members.Count == 0)
            {
                _lobbyRepository.Delete(lobby);
            }
            else
            {
                _lobbyRepository.Update(lobby);
            }
            
            return Unit.Value;
        }
    }
}