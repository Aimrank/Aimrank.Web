using Aimrank.Web.Modules.Cluster.Application.Entities;
using Aimrank.Web.Modules.Cluster.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aimrank.Web.Modules.Cluster.Infrastructure.Application.Repositories
{
    internal class PodRepository : IPodRepository
    {
        private readonly ClusterContext _context;

        public PodRepository(ClusterContext context)
        {
            _context = context;
        }

        public async Task<int> GetAvailableServersCountAsync()
        {
            var result = await _context.Pods
                .Include(p => p.Servers)
                .Select(p => new
                {
                    p.MaxServers,
                    p.Servers.Count
                })
                .ToListAsync();

            return result.Sum(p => p.MaxServers - p.Count);
        }

        public Task<Pod> GetByIpAddressOptionalAsync(string ipAddress)
            => _context.Pods.FirstOrDefaultAsync(p => p.IpAddress == ipAddress);

        public async Task<IEnumerable<Pod>> BrowseAsync()
            => await _context.Pods.Include(p => p.Servers).ToListAsync();

        public void Add(Pod pod) => _context.Pods.Add(pod);

        public void DeleteRange(IEnumerable<Pod> pods) => _context.Pods.RemoveRange(pods);
    }
}