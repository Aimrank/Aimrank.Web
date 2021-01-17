using Aimrank.EventBus;
using Aimrank.Web.Hubs;
using Aimrank.Web.Repositories;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Services
{
    public class EventBusBackgroundService : BackgroundService
    {
        private readonly IGameRepository _gameRepository;
        private readonly ILogger<EventBusBackgroundService> _logger;
        private readonly IHubContext<GameHub, IGameClient> _gameHub;

        public EventBusBackgroundService(
            IGameRepository gameRepository,
            ILogger<EventBusBackgroundService> logger,
            IHubContext<GameHub, IGameClient> gameHub)
        {
            _gameRepository = gameRepository;
            _logger = logger;
            _gameHub = gameHub;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var receiver = new BusReceiver();
            
            receiver.MessageReceived += (_, ea) =>
            {
                _logger.LogInformation($"$MessageReceived: [{ea.Content}]");
                _gameHub.Clients.All.MessageReceived(ea.Content);
                // This all should be async
                // var game = _gameRepository.Get();
                //
                // game.ProcessEvent(ea.Content);
                //
                // foreach (var domainEvent in game.DomainEvents)
                // {
                //     if (domainEvent is GameUpdated @event)
                //     {
                //         var dto = new GameUpdatedDto
                //         {
                //             ScoreT = @event.Game.ScoreT,
                //             ScoreCT = @event.Game.ScoreCT
                //         };
                //
                //         var json = JsonSerializer.Serialize(dto);
                //         
                //         _logger.LogInformation($"Integration event: {json}");
                //         
                //         // Publish domain event to frontend (websockets)
                //     }
                // }
                //
                // game.ClearDomainEvents();
                //
                // _gameRepository.Update(game);
            };
            
            return Task.Run(() => receiver.Listen(), stoppingToken);
        }
    }

    // public class GameUpdatedDto
    // {
    //     public int ScoreT { get; set; }
    //     public int ScoreCT { get; set; }
    // }
}