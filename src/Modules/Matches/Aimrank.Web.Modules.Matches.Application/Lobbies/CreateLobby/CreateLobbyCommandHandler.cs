using Aimrank.Web.Common.Application;
using Aimrank.Web.Modules.Matches.Application.Contracts;
using Aimrank.Web.Modules.Matches.Domain.Lobbies;
using Aimrank.Web.Modules.Matches.Domain.Players;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Application.Lobbies.CreateLobby
{
    internal class CreateLobbyCommandHandler : ICommandHandler<CreateLobbyCommand>
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly ILobbyRepository _lobbyRepository;
        private readonly IPlayerRepository _playerRepository;

        public CreateLobbyCommandHandler(
            IExecutionContextAccessor executionContextAccessor,
            ILobbyRepository lobbyRepository,
            IPlayerRepository playerRepository)
        {
            _executionContextAccessor = executionContextAccessor;
            _lobbyRepository = lobbyRepository;
            _playerRepository = playerRepository;
        }

        public async Task<Unit> Handle(CreateLobbyCommand request, CancellationToken cancellationToken)
        {
            var player = await _playerRepository.GetByIdAsync(new PlayerId(_executionContextAccessor.UserId));
            
            var lobbyId = new LobbyId(request.LobbyId);

            var lobby = await Lobby.CreateAsync(lobbyId, $"team_{_executionContextAccessor.UserId}", player, _lobbyRepository);
            
            _lobbyRepository.Add(lobby);
            
            return Unit.Value;
        }
    }
}