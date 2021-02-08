using Aimrank.Common.Application.Exceptions;
using Aimrank.Domain.Lobbies;
using Aimrank.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aimrank.Infrastructure.Domain.Lobbies
{
    internal class LobbyRepository : ILobbyRepository
    {
        private readonly AimrankContext _context;

        public LobbyRepository(AimrankContext context)
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

        public Task<bool> ExistsForMemberAsync(UserId userId)
            => _context.Lobbies.AnyAsync(l => l.Members.Any(m => m.UserId == userId));

        public void Add(Lobby lobby) => _context.Lobbies.Add(lobby);

        public void Update(Lobby lobby) => _context.Lobbies.Update(lobby);

        public void UpdateRange(IEnumerable<Lobby> lobbies) => _context.Lobbies.UpdateRange(lobbies);

        public void Delete(Lobby lobby) => _context.Lobbies.Remove(lobby);
    }
}