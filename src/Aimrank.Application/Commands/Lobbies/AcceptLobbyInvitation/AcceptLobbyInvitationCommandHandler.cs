using Aimrank.Application.Contracts;
using Aimrank.Common.Application;
using Aimrank.Domain.Lobbies;
using Aimrank.Domain.Users;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Commands.Lobbies.AcceptLobbyInvitation
{
    public class AcceptLobbyInvitationCommandHandler : ICommandHandler<AcceptLobbyInvitationCommand>
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly ILobbyRepository _lobbyRepository;
        private readonly IUserRepository _userRepository;

        public AcceptLobbyInvitationCommandHandler(
            IExecutionContextAccessor executionContextAccessor,
            ILobbyRepository lobbyRepository,
            IUserRepository userRepository)
        {
            _executionContextAccessor = executionContextAccessor;
            _lobbyRepository = lobbyRepository;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(AcceptLobbyInvitationCommand request, CancellationToken cancellationToken)
        {
            var lobby = await _lobbyRepository.GetByIdAsync(new LobbyId(request.LobbyId));
            var invitedUser = await _userRepository.GetByIdAsync(new UserId(_executionContextAccessor.UserId));
            
            lobby.AcceptInvitation(invitedUser);
            
            _lobbyRepository.Update(lobby);
            
            return Unit.Value;
        }
    }
}