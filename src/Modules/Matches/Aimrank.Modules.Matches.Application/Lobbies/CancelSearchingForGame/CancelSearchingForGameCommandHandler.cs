using Aimrank.Common.Application;
using Aimrank.Modules.Matches.Application.Contracts;
using Aimrank.Modules.Matches.Domain.Lobbies;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Modules.Matches.Application.Lobbies.CancelSearchingForGame
{
    internal class CancelSearchingForGameCommandHandler : ICommandHandler<CancelSearchingForGameCommand>
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

            lobby.CancelSearching(_executionContextAccessor.UserId);
            
            _lobbyRepository.Update(lobby);
            
            return Unit.Value;
        }
    }
}