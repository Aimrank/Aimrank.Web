using Aimrank.Modules.Matches.Application.Contracts;
using Aimrank.Modules.Matches.Domain.Players;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Modules.Matches.Application.Players.CreateOrUpdatePlayer
{
    internal class CreateOrUpdatePlayerCommandHandler : ICommandHandler<CreateOrUpdatePlayerCommand>
    {
        private readonly IPlayerRepository _playerRepository;

        public CreateOrUpdatePlayerCommandHandler(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<Unit> Handle(CreateOrUpdatePlayerCommand request, CancellationToken cancellationToken)
        {
            var playerId = new PlayerId(request.PlayerId);

            var player = await _playerRepository.GetByIdOptionalAsync(playerId);
            
            if (player is null)
            {
                player = await Player.CreateAsync(playerId, request.SteamId, _playerRepository);
                
                _playerRepository.Add(player);
                
                return Unit.Value;
            }

            await player.SetSteamIdAsync(request.SteamId, _playerRepository);
            
            return Unit.Value;
        }
    }
}