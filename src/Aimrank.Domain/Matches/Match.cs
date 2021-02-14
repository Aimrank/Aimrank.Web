using Aimrank.Common.Domain;
using Aimrank.Domain.Lobbies;
using Aimrank.Domain.Matches.Events;
using Aimrank.Domain.Users;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Aimrank.Domain.Matches
{
    public class Match : Entity
    {
        private readonly HashSet<MatchLobby> _lobbies = new();
        private readonly HashSet<MatchPlayer> _players = new();
        
        public MatchId Id { get; }
        public int ScoreT { get; private set; }
        public int ScoreCT { get; private set; }
        public string Map { get; private set; }
        public string Address { get; private set; }
        public MatchMode Mode { get; private set; }
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
            MatchMode mode,
            IEnumerable<LobbyId> lobbies)
        {
            Id = id;
            Map = map;
            Mode = mode;
            Status = MatchStatus.Created;
            CreatedAt = DateTime.UtcNow;
            Lobbies = lobbies.Select(l => new MatchLobby(l));
        }

        public void AddPlayer(UserId userId, string steamId, MatchTeam team)
        {
            var player = new MatchPlayer(userId, steamId, team);
            
            _players.Add(player);
        }

        public void UpdateScore(int scoreT, int scoreCT)
        {
            ScoreT = scoreT;
            ScoreCT = scoreCT;
        }

        public void UpdatePlayerStats(string steamId, MatchPlayerStats stats)
        {
            var player = _players.FirstOrDefault(p => p.SteamId == steamId);
            
            player?.UpdateStats(stats);
        }

        public void Finish()
        {
            if (Status == MatchStatus.Finished) return;
            
            Status = MatchStatus.Finished;
            FinishedAt = DateTime.UtcNow;
            
            // Update players rating

            var @event = new MatchFinishedDomainEvent(this, Lobbies.Select(l => l.LobbyId).ToList());
            
            _lobbies.Clear();
            
            AddDomainEvent(@event);
        }

        public void Cancel()
        {
            if (Status == MatchStatus.Canceled) return;
            
            Status = MatchStatus.Canceled;
            FinishedAt = DateTime.UtcNow;
            
            AddDomainEvent(new MatchStatusChangedDomainEvent(this));
        }

        public void SetReady()
        {
            if (Status == MatchStatus.Ready) return;
            
            Status = MatchStatus.Ready;
            
            AddDomainEvent(new MatchStatusChangedDomainEvent(this));
        }

        public void SetStarting(string address)
        {
            if (Status == MatchStatus.Starting) return;
            
            Status = MatchStatus.Starting;
            Address = address;
            
            AddDomainEvent(new MatchStatusChangedDomainEvent(this));
        }

        public void SetStarted()
        {
            if (Status != MatchStatus.Starting) return;

            Status = MatchStatus.Started;
            
            AddDomainEvent(new MatchStatusChangedDomainEvent(this));
        }
    }
}