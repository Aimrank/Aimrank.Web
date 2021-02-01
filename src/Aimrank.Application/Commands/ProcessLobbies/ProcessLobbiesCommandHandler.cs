using Aimrank.Application.CSGO;
using Aimrank.Application.Contracts;
using Aimrank.Application.Events;
using Aimrank.Domain.Lobbies;
using Aimrank.Domain.Matches;
using Aimrank.Domain.Users;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Application.Commands.ProcessLobbies
{
    public class ProcessLobbiesCommandHandler : ICommandHandler<ProcessLobbiesCommand>
    {
        private readonly ILobbyRepository _lobbyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IServerProcessManager _serverProcessManager;
        private readonly IEventDispatcher _eventDispatcher;

        public ProcessLobbiesCommandHandler(
            ILobbyRepository lobbyRepository,
            IUserRepository userRepository,
            IMatchRepository matchRepository,
            IServerProcessManager serverProcessManager,
            IEventDispatcher eventDispatcher)
        {
            _lobbyRepository = lobbyRepository;
            _userRepository = userRepository;
            _matchRepository = matchRepository;
            _serverProcessManager = serverProcessManager;
            _eventDispatcher = eventDispatcher;
        }

        public async Task<Unit> Handle(ProcessLobbiesCommand request, CancellationToken cancellationToken)
        {
            var lobbies = await _lobbyRepository.BrowseAsync(LobbyStatus.Closed);

            foreach (var lobby in lobbies)
            {
                if (lobby.Members.Count < 2)
                {
                    continue;
                }

                var p1 = await _userRepository.GetByIdAsync(lobby.Members.ElementAt(0).UserId);
                var p2 = await _userRepository.GetByIdAsync(lobby.Members.ElementAt(1).UserId);

                var matchId = new MatchId(Guid.NewGuid());

                var match = new Match(matchId, lobby.Configuration.Map, new List<MatchPlayer>
                {
                    new(p1.Id, p1.SteamId, MatchTeam.Terrorists),
                    new(p2.Id, p2.SteamId, MatchTeam.CounterTerrorists)
                });
                
                _matchRepository.Add(match);
                
                // Move to match created event handler - Create and start new server for that match

                var address = _serverProcessManager.StartServer(
                    match.Id.Value,
                    match.Players.Select(p => p.SteamId),
                    match.Map);
                
                // Dispatch event that server was created and send address to users

                var @event = new ServerCreatedEvent(
                    Guid.NewGuid(),
                    match.Id.Value,
                    address,
                    match.Map,
                    match.Players.Select(p => p.Id.Value),
                    DateTime.UtcNow);

                await _eventDispatcher.DispatchAsync(@event);
            }
            
            return Unit.Value;
        }
    }
}