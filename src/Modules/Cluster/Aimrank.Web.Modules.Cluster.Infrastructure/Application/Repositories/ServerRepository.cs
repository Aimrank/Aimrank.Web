using Aimrank.Web.Modules.Cluster.Application.Entities;
using Aimrank.Web.Modules.Cluster.Application.Exceptions;
using Aimrank.Web.Modules.Cluster.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.Modules.Cluster.Infrastructure.Application.Repositories
{
    internal class ServerRepository : IServerRepository
    {
        private readonly ClusterContext _context;

        public ServerRepository(ClusterContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Server>> BrowseByIpAddressesAsync(IEnumerable<string> ipAddresses)
            => await _context.Pods
                .Include(p => p.Servers)
                .Where(p => ipAddresses.Contains(p.IpAddress))
                .SelectMany(p => p.Servers)
                .ToListAsync();

        public async Task<Server> GetByMatchIdAsync(Guid matchId)
        {
            var server = await _context.Servers
                .Include(s => s.Pod)
                .Include(s => s.SteamToken)
                .FirstOrDefaultAsync(s => s.MatchId == matchId);
                
            if (server is null)
            {
                throw new ClusterException($"Server with matchId \"{matchId}\" does not exist.");
            }

            return server;
        }

        public void Add(Server server) => _context.Servers.Add(server);

        public void Delete(Server server) => _context.Servers.Remove(server);

        public void DeleteRange(IEnumerable<Server> servers) => _context.Servers.RemoveRange(servers);
    }
}