using Aimrank.Web.Models;

namespace Aimrank.Web.Repositories
{
    public interface IGameRepository
    {
        Game Get();
        void Update(Game game);
    }
    
    public class GameRepository : IGameRepository
    {
        private Game _game = new();

        public Game Get() => _game;

        public void Update(Game game) => _game = game;
    }
}