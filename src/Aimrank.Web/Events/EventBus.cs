using Aimrank.Web.Data;
using Aimrank.Web.Hubs;
using Aimrank.Web.Server;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.Events
{
    public class Event
    {
        public string Name { get; set; }
    }

    public class MatchEndEventData
    {
        public class MatchEndTeam
        {
            public int Score { get; set; }
            public List<MatchEndPlayer> Clients { get; set; }
        }

        public class MatchEndPlayer
        {
            public string SteamId { get; set; }
            public string Name { get; set; }
            public int Score { get; set; }
            public int Kills { get; set; }
            public int Deaths { get; set; }
            public int Assists { get; set; }
        }

        public MatchEndTeam TeamTerrorists { get; set; }
        public MatchEndTeam TeamCounterTerrorists { get; set; }
    }

    public class MatchEndEvent
    {
        public string Name { get; set; }
        public MatchEndEventData Data { get; set; }
    }
    
    public class EventBus
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public EventBus(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task PublishAsync(Guid serverId, string content)
        {
            var temp = JsonSerializer.Deserialize<Event>(content,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();
            var manager = scope.ServiceProvider.GetRequiredService<ServerProcessManager>();
            var hub = scope.ServiceProvider.GetRequiredService<IHubContext<GameHub, IGameClient>>();
            
            if (temp.Name == "match_end")
            {
                var @event = JsonSerializer.Deserialize<MatchEndEvent>(content,
                    new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});

                await manager.StopServerAsync(serverId);

                var matchId = Guid.NewGuid();
                var players = new List<MatchPlayer>();
                    
                // Teams are rotated here ? (t = 3, ct = 2)
                players.AddRange(@event.Data.TeamTerrorists.Clients.Select(p => new MatchPlayer(p.SteamId, p.Name, matchId, 3, p.Score, p.Kills, p.Deaths, p.Assists)));
                players.AddRange(@event.Data.TeamCounterTerrorists.Clients.Select(p => new MatchPlayer(p.SteamId, p.Name, matchId, 2, p.Score, p.Kills, p.Deaths, p.Assists)));

                var match = new Match(
                    matchId,
                    @event.Data.TeamTerrorists.Score,
                    @event.Data.TeamCounterTerrorists.Score,
                    players,
                    DateTime.UtcNow);

                await context.Matches.AddAsync(match);
                await context.SaveChangesAsync();
            }
            else
            {
                await hub.Clients.All.EventReceived(content);
            }
        }
    }
}