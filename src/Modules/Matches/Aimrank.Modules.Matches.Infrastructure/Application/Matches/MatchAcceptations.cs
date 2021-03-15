using System.Collections.Generic;
using System.Linq;
using System;

namespace Aimrank.Modules.Matches.Infrastructure.Application.Matches
{
    internal class MatchAcceptations
    {
        private readonly HashSet<Guid> _players;
        private readonly HashSet<Guid> _playersAccepted;

        public IEnumerable<Guid> Players => _players;

        public IEnumerable<Guid> PlayersAccepted => _playersAccepted;

        public MatchAcceptations(
            IEnumerable<Guid> players,
            IEnumerable<Guid> playersAccepted)
        {
            _players = new HashSet<Guid>(players);
            _playersAccepted = new HashSet<Guid>(playersAccepted);
        }

        public void Accept(Guid playerId)
        {
            if (_playersAccepted.Contains(playerId))
            {
                return;
            }
            
            if (!_players.Contains(playerId))
            {
                throw new MatchAcceptationException();
            }

            _playersAccepted.Add(playerId);
        }

        public bool IsAccepted() => _players.Count == _playersAccepted.Count;

        public IEnumerable<Guid> GetPendingPlayers() => _players.Except(_playersAccepted);
    }
}