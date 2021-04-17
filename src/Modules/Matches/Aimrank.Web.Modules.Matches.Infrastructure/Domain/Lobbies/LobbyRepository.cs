using Aimrank.Web.Common.Application.Exceptions;
using Aimrank.Web.Modules.Matches.Domain.Lobbies;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Domain.Lobbies
{
    internal class LobbyRepository : ILobbyRepository
    {
        private readonly MatchesContext _context;

        public LobbyRepository(MatchesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Lobby>> BrowseByStatusAsync(LobbyStatus? status)
            => await _context.Lobbies.Where(l => !status.HasValue || l.Status == status).ToListAsync();

        public async Task<IEnumerable<Lobby>> BrowseByIdAsync(IEnumerable<LobbyId> ids)
            => await _context.Lobbies.Where(l => ids.Contains(l.Id)).ToListAsync();

        public async Task<Lobby> GetByIdAsync(LobbyId id)
        {
            var lobby = await _context.Lobbies.FirstOrDefaultAsync(l => l.Id == id);
            if (lobby is null)
            {
                throw new EntityNotFoundException();
            }

            return lobby;
        }

        public Task<bool> ExistsForMemberAsync(Guid userId)
            => _context.Lobbies.AnyAsync(l => l.Members.Any(m => m.PlayerId == userId));

        public void Add(Lobby lobby) => _context.Lobbies.Add(lobby);

        public void Delete(Lobby lobby) => _context.Lobbies.Remove(lobby);
    }
}