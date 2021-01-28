using Aimrank.Application.CSGO;
using Aimrank.Application.Contracts;
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
        private readonly IServerEventNotifier _serverEventNotifier;
        private readonly IMatchRepository _matchRepository;
        
        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public ProcessServerEventCommandHandler(
            IServerProcessManager serverProcessManager,
            IServerEventNotifier serverEventNotifier,
            IMatchRepository matchRepository)
        {
            _serverProcessManager = serverProcessManager;
            _serverEventNotifier = serverEventNotifier;
            _matchRepository = matchRepository;
        }

        public async Task<Unit> Handle(ProcessServerEventCommand request, CancellationToken cancellationToken)
        {
            var temp = JsonSerializer.Deserialize<Event>(request.Content, _jsonSerializerOptions);
            if (temp.Name == "match_end")
            {
                var @event = JsonSerializer.Deserialize<MatchEndEvent>(request.Content, _jsonSerializerOptions);

                await _serverProcessManager.StopServerAsync(request.ServerId);

                var matchId = new MatchId(Guid.NewGuid());
                var players = new List<MatchPlayer>();
                
                // Teams are rotated here ? (t = 3, ct = 2)
                players.AddRange(@event.Data.TeamTerrorists.Clients.Select(p => new MatchPlayer(matchId, p.SteamId, p.Name, MatchTeam.Terrorists, p.Score, p.Kills, p.Deaths, p.Assists)));
                players.AddRange(@event.Data.TeamCounterTerrorists.Clients.Select(p => new MatchPlayer(matchId, p.SteamId, p.Name, MatchTeam.CounterTerrorists, p.Score, p.Kills, p.Deaths, p.Assists)));

                var match = new Match(
                    matchId,
                    @event.Data.TeamTerrorists.Score,
                    @event.Data.TeamCounterTerrorists.Score,
                    DateTime.UtcNow,
                    players);
                
                _matchRepository.Add(match);
            }
            else
            {
                await _serverEventNotifier.NotifyAsync(request.ServerId, request.Content);
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