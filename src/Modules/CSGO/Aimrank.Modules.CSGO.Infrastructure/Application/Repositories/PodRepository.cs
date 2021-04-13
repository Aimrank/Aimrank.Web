using Aimrank.Modules.CSGO.Application.Entities;
using Aimrank.Modules.CSGO.Application.Exceptions;
using Aimrank.Modules.CSGO.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aimrank.Modules.CSGO.Infrastructure.Application.Repositories
{
    internal class PodRepository : IPodRepository
    {
        private readonly CSGOContext _context;

        public PodRepository(CSGOContext context)
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
                throw new CSGOException($"Pod with IP address \"{ipAddress}\" does not exist.");
            }

            return pod;
        }

        public async Task<IEnumerable<Pod>> BrowseAsync()
            => await _context.Pods.Include(p => p.Servers).ToListAsync();

        public void Add(Pod pod) => _context.Pods.Add(pod);

        public void DeleteRange(IEnumerable<Pod> pods) => _context.Pods.RemoveRange(pods);
    }
}