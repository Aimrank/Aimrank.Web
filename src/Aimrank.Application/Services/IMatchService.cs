using Aimrank.Domain.Matches;
using Aimrank.Domain.Users;
using System.Threading.Tasks;

namespace Aimrank.Application.Services
{
    public interface IMatchService
    {
        Task AcceptMatchAsync(Match match, UserId userId);
    }
}