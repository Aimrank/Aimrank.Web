using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application
{
    public interface IUnitOfWork
    {
        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}