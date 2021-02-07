using Aimrank.Application.Contracts;
using Aimrank.Common.Application;
using Aimrank.Domain.Lobbies;
using Aimrank.Domain.Users;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Commands.Lobbies.StartSearchingForGame
{
    public class StartSearchingForGameCommandHandler : ICommandHandler<StartSearchingForGameCommand>
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly ILobbyRepository _lobbyRepository;

        public StartSearchingForGameCommandHandler(
            IExecutionContextAccessor executionContextAccessor,
            ILobbyRepository lobbyRepository)
        {
            _executionContextAccessor = executionContextAccessor;
            _lobbyRepository = lobbyRepository;
        }

        public async Task<Unit> Handle(StartSearchingForGameCommand request, CancellationToken cancellationToken)
        {
            var lobby = await _lobbyRepository.GetByIdAsync(new LobbyId(request.LobbyId));
            var userId = new UserId(_executionContextAccessor.UserId);
            
            lobby.StartSearching(userId);
            
            _lobbyRepository.Update(lobby);
            
            return Unit.Value;
        }
    }
}