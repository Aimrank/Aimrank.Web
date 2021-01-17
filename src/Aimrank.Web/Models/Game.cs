using System.Collections.Generic;
using System.Text.Json;

namespace Aimrank.Web.Models
{
    public interface IGameEvent
    {
        void Process(Game game);
    }
    
    public class GameEvent
    {
        public string Name { get; }

        protected GameEvent(string name) => Name = name;
    }
    
    public class RoundEndGameEvent : GameEvent, IGameEvent
    {
        public int Winner { get; }
        public int Reason { get; }

        public RoundEndGameEvent(string name, int winner, int reason) : base(name)
        {
            Winner = winner;
            Reason = reason;
        }

        public void Process(Game game)
        {
            if (Winner == 1)
            {
                game.SetScoreT(game.ScoreT + 1);
            }
            else
            {
                game.SetScoreCT(game.ScoreCT + 1);
            }
        }
    }
    
    public interface IDomainEvent {}

    public abstract class Entity
    {
        private readonly Queue<IDomainEvent> _domainEvents = new();

        public IEnumerable<IDomainEvent> DomainEvents => _domainEvents;

        public void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Enqueue(domainEvent);
        
        public void ClearDomainEvents() => _domainEvents.Clear();
    }

    public class GameUpdated : IDomainEvent
    {
        public Game Game { get; }

        public GameUpdated(Game game)
        {
            Game = game;
        }
    }

    public class Game : Entity
    {
        private readonly GameEventMapper _mapper = new();
        
        public int ScoreT { get; private set; }
        public int ScoreCT { get; private set; }

        private void Reset()
        {
            ScoreT = 0;
            ScoreCT = 0;
        }

        public void ProcessEvent(string content)
        {
            var @event = _mapper.Map(content);
            @event?.Process(this);
        }

        public void SetScoreT(int score)
        {
            ScoreT = score;
            
            AddDomainEvent(new GameUpdated(this));
        }
        
        public void SetScoreCT(int score)
        {
            ScoreCT = score;
            
            AddDomainEvent(new GameUpdated(this));
        }
    }

    public class GameEventMapper
    {
        public IGameEvent Map(string content)
        {
            var @event = JsonSerializer.Deserialize<GameEvent>(content);

            return @event.Name switch
            {
                "round_end" => JsonSerializer.Deserialize<RoundEndGameEvent>(content),
                _ => null
            };
        }
    }
}