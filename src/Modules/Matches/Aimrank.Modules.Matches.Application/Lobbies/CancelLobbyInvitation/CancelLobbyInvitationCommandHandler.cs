using Aimrank.Common.Application;
using Aimrank.Modules.Matches.Application.Contracts;
using Aimrank.Modules.Matches.Domain.Lobbies;
using Aimrank.Modules.Matches.Domain.Players;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Modules.Matches.Application.Lobbies.CancelLobbyInvitation
{
    internal class CancelLobbyInvitationCommandHandler : ICommandHandler<CancelLobbyInvitationCommand>
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly ILobbyRepository _lobbyRepository;
        private readonly IPlayerRepository _playerRepository;

        public CancelLobbyInvitationCommandHandler(
            IExecutionContextAccessor executionContextAccessor,
            ILobbyRepository lobbyRepository,
            IPlayerRepository playerRepository)
        {
            _executionContextAccessor = executionContextAccessor;
            _lobbyRepository = lobbyRepository;
            _playerRepository = playerRepository;
        }

        public async Task<Unit> Handle(CancelLobbyInvitationCommand request, CancellationToken cancellationToken)
        {
            var lobby = await _lobbyRepository.GetByIdAsync(new LobbyId(request.LobbyId));
            var invitedPlayer = await _playerRepository.GetByIdAsync(new PlayerId(_executionContextAccessor.UserId));
            
            lobby.CancelInvitation(invitedPlayer);
            
            return Unit.Value;
        }
    }
}