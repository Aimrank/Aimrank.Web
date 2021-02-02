using Aimrank.Common.Application.Events;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Common.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private readonly IEventDispatcher _eventDispatcher;

        public UnitOfWork(DbContext context, IEventDispatcher eventDispatcher)
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