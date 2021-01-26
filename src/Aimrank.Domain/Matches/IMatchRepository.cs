using System.Threading.Tasks;
using System;

namespace Aimrank.Domain.Matches
{
    public interface IMatchRepository
    {
        Task<Match> GetByIdAsync(Guid id);
        void Add(Match match);
        void Update(Match match);
        void Delete(Match match);
    }
}