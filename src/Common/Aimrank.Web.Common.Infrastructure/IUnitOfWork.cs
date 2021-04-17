using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Common.Infrastructure
{
    public interface IUnitOfWork
    {
        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}