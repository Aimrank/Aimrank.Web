using Aimrank.Application.Contracts;
using Aimrank.Common.Application;
using Aimrank.Domain.Lobbies;
using Aimrank.Domain.Users;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Commands.Lobbies.CancelSearchingForGame
{
    public class CancelSearchingForGameCommandHandler : ICommandHandler<CancelSearchingForGameCommand>
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly ILobbyRepository _lobbyRepository;

        public CancelSearchingForGameCommandHandler(
            IExecutionContextAccessor executionContextAccessor,
            ILobbyRepository lobbyRepository)
        {
            _executionContextAccessor = executionContextAccessor;
            _lobbyRepository = lobbyRepository;
        }

        public async Task<Unit> Handle(CancelSearchingForGameCommand request, CancellationToken cancellationToken)
        {
            var lobby = await _lobbyRepository.GetByIdAsync(new LobbyId(request.LobbyId));
            var userId = new UserId(_executionContextAccessor.UserId);

            lobby.CancelSearching(userId);
            
            _lobbyRepository.Update(lobby);
            
            return Unit.Value;
        }
    }
}