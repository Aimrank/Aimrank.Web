using Aimrank.Web.Modules.Cluster.Application.Entities;
using Aimrank.Web.Modules.Cluster.Application.Exceptions;
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

        public Task<int> GetAvailableServersCountAsync()
            => _context.Pods.Select(p => p.MaxServers - p.Servers.Count).SumAsync();

        public async Task<Pod> GetByIpAddressAsync(string ipAddress)
        {
            var pod = await _context.Pods.FirstOrDefaultAsync(p => p.IpAddress == ipAddress);
            if (pod is null)
            {
                throw new ClusterException($"Pod with IP address \"{ipAddress}\" does not exist.");
            }

            return pod;
        }

        public async Task<IEnumerable<Pod>> BrowseAsync()
            => await _context.Pods.Include(p => p.Servers).ToListAsync();

        public void Add(Pod pod) => _context.Pods.Add(pod);

        public void DeleteRange(IEnumerable<Pod> pods) => _context.Pods.RemoveRange(pods);
    }
}