using Aimrank.Web.Modules.Cluster.Application.Contracts;
using Aimrank.Web.Modules.Cluster.Application.Queries.GetAvailableServers;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Cluster.Infrastructure.Application.Queries.GetAvailableServers
{
    internal class GetAvailableServersQueryHandler : IQueryHandler<GetAvailableServersQuery, int>
    {
        private readonly ClusterContext _context;

        public GetAvailableServersQueryHandler(ClusterContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(GetAvailableServersQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Pods
                .Include(p => p.Servers)
                .Select(p => new
                {
                    p.MaxServers,
                    p.Servers.Count
                })
                .ToListAsync(cancellationToken);

            return result.Sum(p => p.MaxServers - p.Count);
        }
    }
}