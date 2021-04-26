using Aimrank.Web.Modules.Cluster.Application.Entities;
using Aimrank.Web.Modules.Cluster.Application.Exceptions;
using Aimrank.Web.Modules.Cluster.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aimrank.Web.Modules.Cluster.Infrastructure.Application.Repositories
{
    internal class SteamTokenRepository : ISteamTokenRepository
    {
        private readonly ClusterContext _context;

        public SteamTokenRepository(ClusterContext context)
        {
            _context = context;
        }

        public async Task<SteamToken> GetAsync(string token)
        {
            var steamToken = await _context.SteamTokens
                .Include(t => t.Server)
                .FirstOrDefaultAsync(t => t.Token == token);

            if (steamToken is null)
            {
                throw new ClusterException($"Steam token \"{token}\" does not exist.");
            }

            return steamToken;
        }

        public Task<SteamToken> GetOptionalAsync(string token)
            => _context.SteamTokens
                .Include(t => t.Server)
                .FirstOrDefaultAsync(t => t.Token == token);

        public async Task<IEnumerable<SteamToken>> BrowseUnusedAsync(int limit)
        {
            var used = await _context.Servers.Select(s => s.SteamToken.Token).ToListAsync();
            return await _context.SteamTokens.Where(t => !used.Contains(t.Token)).Take(limit).ToListAsync();
        }

        public void Add(SteamToken steamToken) => _context.SteamTokens.Add(steamToken);

        public void Delete(SteamToken steamToken) => _context.SteamTokens.Remove(steamToken);
    }
}