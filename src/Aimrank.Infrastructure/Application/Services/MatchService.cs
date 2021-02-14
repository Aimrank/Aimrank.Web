using Aimrank.Application.CSGO;
using Aimrank.Application.Services;
using Aimrank.Common.Infrastructure.EventBus;
using Aimrank.Domain.Matches;
using Aimrank.Domain.Users;
using Aimrank.Infrastructure.Configuration.Redis;
using Aimrank.IntegrationEvents.Matches;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Aimrank.Infrastructure.Application.Services
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
            var key = $"acceptations:{match.Id}";
                
            var acceptations = await _database.GetJsonAsync<MatchAcceptations>(key);
            if (acceptations is null)
            {
                acceptations = new MatchAcceptations();
            }

            if (match.Players.All(p => p.UserId != userId))
            {
                throw new Exception("You cannot accept this match");
            }

            acceptations.Users.Add(userId);


            if (acceptations.Users.Count != match.Players.Count())
            {
                await _database.SetJsonAsync(key, acceptations, TimeSpan.FromSeconds(10));
            }
            else
            {
                await _database.KeyDeleteAsync(key);
                
                var address = _serverProcessManager.StartServer(
                   match.Id, match.Map, match.Players.Select(p => $"{p.SteamId}:{p.Team}"));
                
                match.SetStarting(address);
            }

            await _eventBus.Publish(new MatchAcceptedEvent(match.Id, userId, match.Lobbies.Select(l => l.LobbyId.Value)));
        }
    }

    public class MatchAcceptations
    {
        public HashSet<Guid> Users { get; init; } = new();
    }
}