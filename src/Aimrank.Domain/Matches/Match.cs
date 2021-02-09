using Aimrank.Common.Domain;
using Aimrank.Domain.Matches.Events;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Aimrank.Domain.Matches
{
    public class Match : Entity
    {
        private readonly HashSet<MatchLobby> _lobbies = new();
        private readonly HashSet<MatchTeam> _teams = new();
        
        public MatchId Id { get; }
        public string Map { get; private set; }
        public string Address { get; private set; }
        public MatchMode Mode { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? FinishedAt { get; private set; }
        public MatchStatus Status { get; private set; }

        public IEnumerable<MatchTeam> Teams
        {
            get => _teams;
            private init => _teams = new HashSet<MatchTeam>(value);
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
            MatchMode mode,
            IEnumerable<MatchTeam> teams,
            IEnumerable<MatchLobby> lobbies)
        {
            Id = id;
            Map = map;
            Teams = teams;
            Lobbies = lobbies;
            Status = MatchStatus.Created;
            CreatedAt = DateTime.UtcNow;
        }

        public void Finish(MatchTeam team1, MatchTeam team2)
        {
            var p1 = team1.Players.FirstOrDefault().SteamId;
            var p2 = team2.Players.FirstOrDefault().SteamId;
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