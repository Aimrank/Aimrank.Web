using Aimrank.Application.Contracts;
using Aimrank.Common.Application;
using Aimrank.Domain.Lobbies;
using Aimrank.Domain.Users;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Commands.JoinLobby
{
    public class JoinLobbyCommandHandler : ICommandHandler<JoinLobbyCommand>
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly ILobbyRepository _lobbyRepository;
        private readonly IUserRepository _userRepository;

        public JoinLobbyCommandHandler(
            IExecutionContextAccessor executionContextAccessor,
            ILobbyRepository lobbyRepository,
            IUserRepository userRepository)
        {
            _executionContextAccessor = executionContextAccessor;
            _lobbyRepository = lobbyRepository;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(JoinLobbyCommand request, CancellationToken cancellationToken)
        {
            var userId = new UserId(_executionContextAccessor.UserId);
            var user = await _userRepository.GetByIdAsync(userId);
            
            var lobbyId = new LobbyId(request.Id);
            var lobby = await _lobbyRepository.GetByIdAsync(lobbyId);
            
            await lobby.JoinAsync(user, _lobbyRepository);
            
            _lobbyRepository.Update(lobby);
            
            return Unit.Value;
        }
    }
}