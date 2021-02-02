using System.Threading.Tasks;

namespace Aimrank.Domain.Matches
{
    public interface IMatchRepository
    {
        Task<Match> GetByIdAsync(MatchId id);
        void Add(Match match);
        void Update(Match match);
        void Delete(Match match);
    }
}