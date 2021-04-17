using Aimrank.Web.Modules.CSGO.Application.Entities;
using Aimrank.Web.Modules.CSGO.Application.Exceptions;
using Aimrank.Web.Modules.CSGO.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.Modules.CSGO.Infrastructure.Application.Repositories
{
    internal class ServerRepository : IServerRepository
    {
        private readonly CSGOContext _context;

        public ServerRepository(CSGOContext context)
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
            var server = await _context.Servers.FirstOrDefaultAsync(s => s.MatchId == matchId);
            if (server is null)
            {
                throw new CSGOException($"Server with matchId \"{matchId}\" does not exist.");
            }

            return server;
        }

        public void Delete(Server server) => _context.Servers.Remove(server);

        public void DeleteRange(IEnumerable<Server> servers) => _context.Servers.RemoveRange(servers);
    }
}