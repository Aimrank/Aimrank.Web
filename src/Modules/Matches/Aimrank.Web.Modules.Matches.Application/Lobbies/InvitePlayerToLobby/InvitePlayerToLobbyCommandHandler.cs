using Aimrank.Web.Common.Application;
using Aimrank.Web.Modules.Matches.Application.Contracts;
using Aimrank.Web.Modules.Matches.Domain.Lobbies;
using Aimrank.Web.Modules.Matches.Domain.Players;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Application.Lobbies.InvitePlayerToLobby
{
    internal class InvitePlayerToLobbyCommandHandler : ICommandHandler<InvitePlayerToLobbyCommand>
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly ILobbyRepository _lobbyRepository;
        private readonly IPlayerRepository _playerRepository;

        public InvitePlayerToLobbyCommandHandler(
            IExecutionContextAccessor executionContextAccessor,
            ILobbyRepository lobbyRepository,
            IPlayerRepository playerRepository)
        {
            _executionContextAccessor = executionContextAccessor;
            _lobbyRepository = lobbyRepository;
            _playerRepository = playerRepository;
        }

        public async Task<Unit> Handle(InvitePlayerToLobbyCommand request, CancellationToken cancellationToken)
        {
            var lobby = await _lobbyRepository.GetByIdAsync(new LobbyId(request.LobbyId));
            var invitingPlayer = await _playerRepository.GetByIdAsync(new PlayerId(_executionContextAccessor.UserId));
            var invitedPlayer = await _playerRepository.GetByIdAsync(new PlayerId(request.InvitedPlayerId));
            
            lobby.Invite(invitingPlayer, invitedPlayer);
            
            return Unit.Value;
        }
    }
}