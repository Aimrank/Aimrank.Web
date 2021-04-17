using Aimrank.Web.Common.Application;
using Aimrank.Web.Modules.Matches.Application.Contracts;
using Aimrank.Web.Modules.Matches.Domain.Lobbies;
using Aimrank.Web.Modules.Matches.Domain.Players;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Application.Lobbies.KickPlayerFromLobby
{
    internal class KickPlayerFromLobbyCommandHandler : ICommandHandler<KickPlayerFromLobbyCommand>
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly ILobbyRepository _lobbyRepository;

        public KickPlayerFromLobbyCommandHandler(
            IExecutionContextAccessor executionContextAccessor,
            ILobbyRepository lobbyRepository)
        {
            _executionContextAccessor = executionContextAccessor;
            _lobbyRepository = lobbyRepository;
        }

        public async Task<Unit> Handle(KickPlayerFromLobbyCommand request, CancellationToken cancellationToken)
        {
            var lobby = await _lobbyRepository.GetByIdAsync(new LobbyId(request.LobbyId));

            var kickingPlayerId = new PlayerId(_executionContextAccessor.UserId);
            var kickedPlayerId = new PlayerId(request.PlayerId);

            lobby.Kick(kickingPlayerId, kickedPlayerId);
            
            return Unit.Value;
        }
    }
}