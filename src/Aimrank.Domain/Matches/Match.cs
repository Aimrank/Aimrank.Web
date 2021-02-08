using Aimrank.Common.Domain;
using Aimrank.Domain.Matches.Events;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Aimrank.Domain.Matches
{
    public class Match : Entity
    {
        private readonly HashSet<MatchPlayer> _players = new();
        private readonly HashSet<MatchLobby> _lobbies = new();
        
        public MatchId Id { get; }
        public int ScoreT { get; private set; }
        public int ScoreCT { get; private set; }
        public string Map { get; private set; }
        public string Address { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? FinishedAt { get; private set; }
        public MatchStatus Status { get; private set; }

        public IEnumerable<MatchPlayer> Players
        {
            get => _players;
            private init => _players = new HashSet<MatchPlayer>(value);
        }

        public IEnumerable<MatchLobby> Lobbies
        {
            get => _lobbies;
            private init => _lobbies = new HashSet<MatchLobby>(value);
        }

        private Match() {}

        public Match(
            MatchId id,
            string map,
            IEnumerable<MatchPlayer> players,
            IEnumerable<MatchLobby> lobbies)
        {
            Id = id;
            Map = map;
            Players = players;
            Lobbies = lobbies;
            Status = MatchStatus.Created;
            CreatedAt = DateTime.UtcNow;
        }

        public void Finish(int scoreT, int scoreCT)
        {
            ScoreT = scoreT;
            ScoreCT = scoreCT;
            Status = MatchStatus.Finished;
            FinishedAt = DateTime.UtcNow;

            var @event = new MatchFinishedDomainEvent(this, Lobbies.Select(l => l.LobbyId).ToList());
            
            _lobbies.Clear();
            
            AddDomainEvent(@event);
        }

        public void Cancel()
        {
            Status = MatchStatus.Canceled;
            FinishedAt = DateTime.UtcNow;
            
            AddDomainEvent(new MatchCanceledDomainEvent(this));
        }

        public void Start(string address)
        {
            Status = MatchStatus.Starting;
            Address = address;
            
            AddDomainEvent(new MatchStartingDomainEvent(this));
        }

        public void MarkAsStarted()
        {
            if (Status != MatchStatus.Starting)
            {
                return;
            }
            
            Status = MatchStatus.Started;
                
            AddDomainEvent(new MatchStartedDomainEvent(this));
        }
    }
}