using Aimrank.Application.Contracts;
using Aimrank.Common.Application;
using Aimrank.Domain.Lobbies;
using Aimrank.Domain.Users;
using MediatR;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Commands.Lobbies.LeaveLobby
{
    internal class LeaveLobbyCommandHandler : ICommandHandler<LeaveLobbyCommand>
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly ILobbyRepository _lobbyRepository;

        public LeaveLobbyCommandHandler(
            IExecutionContextAccessor executionContextAccessor,
            ILobbyRepository lobbyRepository)
        {
            _executionContextAccessor = executionContextAccessor;
            _lobbyRepository = lobbyRepository;
        }

        public async Task<Unit> Handle(LeaveLobbyCommand request, CancellationToken cancellationToken)
        {
            var lobby = await _lobbyRepository.GetByIdAsync(new LobbyId(request.LobbyId));
            var userId = new UserId(_executionContextAccessor.UserId);
            
            lobby.Leave(userId);

            if (lobby.Members.Any())
            {
                _lobbyRepository.Update(lobby);
            }
            else
            {
                _lobbyRepository.Delete(lobby);
            }
            
            return Unit.Value;
        }
    }
}