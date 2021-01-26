using Aimrank.Application;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Infrastructure.Application
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly AimrankContext _context;

        public UnitOfWork(AimrankContext context)
        {
            _context = context;
        }

        public Task CommitAsync(CancellationToken cancellationToken = default)
            => _context.SaveChangesAsync(cancellationToken);
    }
}