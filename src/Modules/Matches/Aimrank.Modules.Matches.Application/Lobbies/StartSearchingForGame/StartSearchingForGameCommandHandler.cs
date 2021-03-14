using Aimrank.Common.Application;
using Aimrank.Modules.Matches.Application.Contracts;
using Aimrank.Modules.Matches.Domain.Lobbies;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Modules.Matches.Application.Lobbies.StartSearchingForGame
{
    internal class StartSearchingForGameCommandHandler : ICommandHandler<StartSearchingForGameCommand>
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
            
            lobby.StartSearching(_executionContextAccessor.UserId);
            
            _lobbyRepository.Update(lobby);
            
            return Unit.Value;
        }
    }
}