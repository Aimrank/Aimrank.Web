using Aimrank.Web.Modules.CSGO.Application.Entities;
using Aimrank.Web.Modules.CSGO.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aimrank.Web.Modules.CSGO.Infrastructure.Application.Repositories
{
    internal class SteamTokenRepository : ISteamTokenRepository
    {
        private readonly CSGOContext _context;

        public SteamTokenRepository(CSGOContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SteamToken>> BrowseUnusedAsync(int limit)
        {
            var used = await _context.Servers.Select(s => s.SteamToken.Token).ToListAsync();
            return await _context.SteamTokens.Where(t => !used.Contains(t.Token)).Take(limit).ToListAsync();
        }
    }
}