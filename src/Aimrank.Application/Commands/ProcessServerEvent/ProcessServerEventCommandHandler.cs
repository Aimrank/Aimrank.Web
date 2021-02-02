using Aimrank.Application.CSGO;
using Aimrank.Application.Contracts;
using Aimrank.Application.Events;
using Aimrank.Domain.Matches;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Application.Commands.ProcessServerEvent
{
    public class ProcessServerEventCommandHandler : ICommandHandler<ProcessServerEventCommand>
    {
        private readonly IServerProcessManager _serverProcessManager;
        private readonly IMatchRepository _matchRepository;
        private readonly IEventDispatcher _eventDispatcher;
        
        private readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public ProcessServerEventCommandHandler(
            IServerProcessManager serverProcessManager,
            IMatchRepository matchRepository,
            IEventDispatcher eventDispatcher)
        {
            _serverProcessManager = serverProcessManager;
            _matchRepository = matchRepository;
            _eventDispatcher = eventDispatcher;
        }

        public async Task<Unit> Handle(ProcessServerEventCommand request, CancellationToken cancellationToken)
        {
            var temp = JsonSerializer.Deserialize<Event>(request.Content, _jsonSerializerOptions);
            if (temp.Name == "match_end")
            {
                var @event = JsonSerializer.Deserialize<MatchEndEvent>(request.Content, _jsonSerializerOptions);
                
                await _serverProcessManager.StopServerAsync(request.ServerId);

                var match = await _matchRepository.GetByIdAsync(new MatchId(request.ServerId));
                

                foreach (var player in match.Players)
                {
                    var client =
                        @event.Data.TeamTerrorists.Clients.FirstOrDefault(c => c.SteamId == player.SteamId) ??
                        @event.Data.TeamCounterTerrorists.Clients.FirstOrDefault(c => c.SteamId == player.SteamId);

                    if (client is null)
                    {
                        continue;
                    }

                    player.UpdateStats(client.Kills, client.Assists, client.Deaths, client.Score);
                }
                
                match.Finish(
                    @event.Data.TeamTerrorists.Score,
                    @event.Data.TeamCounterTerrorists.Score);
                
                _matchRepository.Update(match);
            }
            else
            {
                await _eventDispatcher.DispatchAsync(new ServerMessageReceivedEvent(Guid.NewGuid(), request.ServerId,
                    request.Content, DateTime.UtcNow));
            }
            
            return Unit.Value;
        }

        private class Event
        {
            public string Name { get; set; }
        }

        private class MatchEndEventData
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

        private class MatchEndEvent
        {
            public string Name { get; set; }
            public MatchEndEventData Data { get; set; }
        }
    }
}