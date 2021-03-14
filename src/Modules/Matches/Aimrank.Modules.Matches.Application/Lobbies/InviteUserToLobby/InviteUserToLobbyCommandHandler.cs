using Aimrank.Common.Application;
using Aimrank.Modules.Matches.Application.Contracts;
using Aimrank.Modules.Matches.Domain.Lobbies;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Modules.Matches.Application.Lobbies.InviteUserToLobby
{
    internal class InviteUserToLobbyCommandHandler : ICommandHandler<InviteUserToLobbyCommand>
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly ILobbyRepository _lobbyRepository;
        private readonly IUserRepository _userRepository;

        public InviteUserToLobbyCommandHandler(
            IExecutionContextAccessor executionContextAccessor,
            ILobbyRepository lobbyRepository,
            IUserRepository userRepository)
        {
            _executionContextAccessor = executionContextAccessor;
            _lobbyRepository = lobbyRepository;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(InviteUserToLobbyCommand request, CancellationToken cancellationToken)
        {
            var lobby = await _lobbyRepository.GetByIdAsync(new LobbyId(request.LobbyId));
            var invitingUser = await _userRepository.GetByIdAsync(new UserId(_executionContextAccessor.UserId));
            var invitedUser = await _userRepository.GetByIdAsync(new UserId(request.InvitedUserId));
            
            lobby.Invite(invitingUser, invitedUser);
            
            _lobbyRepository.Update(lobby);
            
            return Unit.Value;
        }
    }
}