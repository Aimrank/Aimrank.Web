using Aimrank.Common.Application;
using Aimrank.Modules.Matches.Application.Contracts;
using Aimrank.Modules.Matches.Domain.Lobbies;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Modules.Matches.Application.Lobbies.CancelLobbyInvitation
{
    internal class CancelLobbyInvitationCommandHandler : ICommandHandler<CancelLobbyInvitationCommand>
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly ILobbyRepository _lobbyRepository;
        private readonly IUserRepository _userRepository;

        public CancelLobbyInvitationCommandHandler(
            IExecutionContextAccessor executionContextAccessor,
            ILobbyRepository lobbyRepository,
            IUserRepository userRepository)
        {
            _executionContextAccessor = executionContextAccessor;
            _lobbyRepository = lobbyRepository;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(CancelLobbyInvitationCommand request, CancellationToken cancellationToken)
        {
            var lobby = await _lobbyRepository.GetByIdAsync(new LobbyId(request.LobbyId));
            var invitedUser = await _userRepository.GetByIdAsync(new UserId(_executionContextAccessor.UserId));
            
            lobby.CancelInvitation(invitedUser);
            
            _lobbyRepository.Update(lobby);
            
            return Unit.Value;
        }
    }
}