using Aimrank.Common.Infrastructure.EventBus;
using Aimrank.Modules.Matches.Application.CSGO;
using Aimrank.Modules.Matches.Application.Matches;
using Aimrank.Modules.Matches.Domain.Matches;
using Aimrank.Modules.Matches.Infrastructure.Configuration.Redis;
using Aimrank.Modules.Matches.IntegrationEvents.Matches;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Aimrank.Modules.Matches.Infrastructure.Application.Matches
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
        
        public async Task AcceptMatchAsync(Match match, Guid playerId)
        {
            var key = GetKey(match.Id);

            var acceptations = await _database.GetJsonAsync<MatchAcceptations>(key) ??
                               new MatchAcceptations(match.Players.Select(m => m.PlayerId.Value),
                                   Enumerable.Empty<Guid>());

            acceptations.Accept(playerId);

            if (acceptations.IsAccepted())
            {
                await _database.KeyDeleteAsync(key);

                var address = _serverProcessManager.StartServer(
                    match.Id, match.Map, match.Players.Select(p => $"{p.SteamId}:{(int) p.Team}"));
                
                match.SetStarting(address);
            }
            else
            {
                await _database.SetJsonAsync(key, acceptations, TimeSpan.FromMinutes(1));
            }

            await _eventBus.Publish(new MatchAcceptedEvent(match.Id, playerId, match.Lobbies.Select(l => l.LobbyId.Value)));
        }

        public async Task<IEnumerable<Guid>> GetNotAcceptedPlayersAsync(MatchId matchId)
        {
            var key = GetKey(matchId);

            var acceptations = await _database.GetJsonAsync<MatchAcceptations>(key);
            return acceptations is null ? Enumerable.Empty<Guid>() : acceptations.GetPendingPlayers();
        }

        private static string GetKey(MatchId matchId) => $"acceptations:{matchId.Value}";
    }
}