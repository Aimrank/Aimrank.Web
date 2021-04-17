using Aimrank.Web.Common.Domain;
using Aimrank.Web.Modules.Matches.Domain.Lobbies;
using Aimrank.Web.Modules.Matches.Domain.Matches.Events;
using Aimrank.Web.Modules.Matches.Domain.Players;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Aimrank.Web.Modules.Matches.Domain.Matches
{
    public class Match : Entity, IAggregateRoot
    {
        private readonly HashSet<MatchLobby> _lobbies = new();
        private readonly HashSet<MatchPlayer> _players = new();
        
        public MatchId Id { get; }
        private MatchWinner _winner;
        private int _scoreT;
        private int _scoreCT;
        private string _address;
        private DateTime _createdAt;
        public string Map { get; }
        public MatchMode Mode { get; }
        public MatchStatus Status { get; private set; }
        public DateTime? FinishedAt { get; private set; }

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
            _createdAt = DateTime.UtcNow;
            Lobbies = lobbies.Select(l => new MatchLobby(l));
        }

        public void AddPlayer(PlayerId playerId, string steamId, MatchTeam team, int rating = 1200)
        {
            var player = new MatchPlayer(playerId, steamId, team, rating);
            
            _players.Add(player);
        }

        public void UpdateScore(MatchWinner winner, int scoreT, int scoreCT)
        {
            _winner = winner;
            _scoreT = scoreT;
            _scoreCT = scoreCT;
        }

        public void UpdatePlayerStats(string steamId, MatchPlayerStats stats)
        {
            var player = _players.FirstOrDefault(p => p.SteamId == steamId);
            
            player?.UpdateStats(stats);
        }

        public void MarkPlayerAsLeaver(string steamId)
        {
            var player = _players.FirstOrDefault(p => p.SteamId == steamId);
            if (player is not null)
            {
                player.MarkAsLeaver();
                
                AddDomainEvent(new MatchPlayerLeftDomainEvent(player, _lobbies));
            }
        }

        public void Finish()
        {
            if (Status == MatchStatus.Finished) return;
            
            Status = MatchStatus.Finished;
            FinishedAt = DateTime.UtcNow;

            var avgT = (int) Players.Where(p => p.Team == MatchTeam.T).Average(p => p.RatingStart);
            var avgCT = (int) Players.Where(p => p.Team == MatchTeam.CT).Average(p => p.RatingStart);
            
            foreach (var player in Players)
            {
                double score;

                if (_winner == MatchWinner.Draw)
                {
                    score = 0.5;
                }
                else
                {
                    score = player.Team == MatchTeam.T && _winner == MatchWinner.T ||
                            player.Team == MatchTeam.CT && _winner == MatchWinner.CT
                        ? 1
                        : 0;
                }
                
                player.CalculateNewRating(score, player.Team == MatchTeam.T ? avgCT : avgT);
            }

            var @event = new MatchFinishedDomainEvent(this, _scoreT, _scoreCT, Lobbies.Select(l => l.LobbyId).ToList());
            
            _lobbies.Clear();
            
            AddDomainEvent(@event);
        }

        public void SetReady()
        {
            if (Status == MatchStatus.Ready) return;
            
            Status = MatchStatus.Ready;
            
            AddDomainEvent(new MatchReadyDomainEvent(this, Map));
        }

        public void SetStarting(string address)
        {
            if (Status == MatchStatus.Starting) return;
            
            Status = MatchStatus.Starting;
            _address = address;
            
            AddDomainEvent(new MatchStartingDomainEvent(this));
        }

        public void SetStarted()
        {
            if (Status != MatchStatus.Starting) return;

            Status = MatchStatus.Started;
            
            AddDomainEvent(new MatchStartedDomainEvent(this, Map, _address));
        }
    }
}