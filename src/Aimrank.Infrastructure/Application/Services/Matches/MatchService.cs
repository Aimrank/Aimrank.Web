using Aimrank.Application.CSGO;
using Aimrank.Application.Services;
using Aimrank.Common.Infrastructure.EventBus;
using Aimrank.Domain.Matches;
using Aimrank.Domain.Users;
using Aimrank.Infrastructure.Configuration.Redis;
using Aimrank.IntegrationEvents.Matches;
using StackExchange.Redis;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Aimrank.Infrastructure.Application.Services.Matches
{
    internal class MatchService : IMatchService
    {
        private readonly IServerProcessManager _serverProcessManager;
        private readonly IDatabase _database;
        private readonly IEventBus _eventBus;

        public MatchService(
            IServerProcessManager serverProcessManager,
            IConnectionMultiplexer connectionMultiplexer,
            IEventBus eventBus)
        {
            _serverProcessManager = serverProcessManager;
            _database = connectionMultiplexer.GetDatabase();
            _eventBus = eventBus;
        }
        
        public async Task AcceptMatchAsync(Match match, UserId userId)
        {
            var key = $"acceptations:{match.Id.Value}";

            var acceptations = await _database.GetJsonAsync<MatchAcceptations>(key) ??
                               new MatchAcceptations(match.Players.Select(p => p.UserId.Value),
                                   Enumerable.Empty<Guid>());

            acceptations.Accept(userId);

            if (acceptations.IsAccepted())
            {
                await _database.KeyDeleteAsync(key);
                
                var address = _serverProcessManager.StartServer(
                   match.Id, match.Map, match.Players.Select(p => $"{p.SteamId}:{p.Team}"));
                
                match.SetStarting(address);
            }
            else
            {
                await _database.SetJsonAsync(key, acceptations, TimeSpan.FromSeconds(30));
            }

            await _eventBus.Publish(new MatchAcceptedEvent(match.Id, userId, match.Lobbies.Select(l => l.LobbyId.Value)));
        }
    }
}