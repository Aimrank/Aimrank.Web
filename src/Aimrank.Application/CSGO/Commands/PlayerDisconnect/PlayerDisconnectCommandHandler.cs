using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Application.CSGO.Commands.PlayerDisconnect
{
    public class PlayerDisconnectCommandHandler : IServerEventCommandHandler<PlayerDisconnectCommand>
    {
        public Task<Unit> Handle(PlayerDisconnectCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Player disconnected from match ({request.MatchId}) and failed to reconnect within specified time: {request.SteamId}");
            
            return Task.FromResult(Unit.Value);
        }
    }
}