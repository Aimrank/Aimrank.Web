using Aimrank.Application;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Infrastructure.Application
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly AimrankContext _context;
        private readonly IEventDispatcher _eventDispatcher;

        public UnitOfWork(AimrankContext context, IEventDispatcher eventDispatcher)
        {
            _context = context;
            _eventDispatcher = eventDispatcher;
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _eventDispatcher.DispatchAsync();
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}