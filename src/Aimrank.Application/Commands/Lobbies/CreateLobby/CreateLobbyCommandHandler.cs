using Aimrank.Application.Contracts;
using Aimrank.Common.Application;
using Aimrank.Domain.Lobbies;
using Aimrank.Domain.Users;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Commands.Lobbies.CreateLobby
{
    public class CreateLobbyCommandHandler : ICommandHandler<CreateLobbyCommand>
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly ILobbyRepository _lobbyRepository;
        private readonly IUserRepository _userRepository;

        public CreateLobbyCommandHandler(
            IExecutionContextAccessor executionContextAccessor,
            ILobbyRepository lobbyRepository,
            IUserRepository userRepository)
        {
            _executionContextAccessor = executionContextAccessor;
            _lobbyRepository = lobbyRepository;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(CreateLobbyCommand request, CancellationToken cancellationToken)
        {
            var userId = new UserId(_executionContextAccessor.UserId);
            var user = await _userRepository.GetByIdAsync(userId);
            
            var lobbyId = new LobbyId(request.LobbyId);
            var lobby = await Lobby.CreateAsync(lobbyId, user, _lobbyRepository);
            
            _lobbyRepository.Add(lobby);
            
            return Unit.Value;
        }
    }
}