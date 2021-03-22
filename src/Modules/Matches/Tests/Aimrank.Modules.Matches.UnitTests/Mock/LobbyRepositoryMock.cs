using Aimrank.Common.Application.Exceptions;
using Aimrank.Modules.Matches.Domain.Lobbies;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Aimrank.Modules.Matches.UnitTests.Mock
{
    internal class LobbyRepositoryMock : ILobbyRepository
    {
        private readonly Dictionary<LobbyId, Lobby> _lobbies = new();

        public Task<IEnumerable<Lobby>> BrowseByStatusAsync(LobbyStatus? status)
            => Task.FromResult(_lobbies.Values.Where(l => !status.HasValue || l.Status == status));

        public Task<IEnumerable<Lobby>> BrowseByIdAsync(IEnumerable<LobbyId> ids)
            => Task.FromResult(_lobbies.Values.Where(l => ids.Contains(l.Id)));

        public Task<Lobby> GetByIdAsync(LobbyId id)
        {
            var lobby = _lobbies.GetValueOrDefault(id);
            if (lobby is null)
            {
                throw new EntityNotFoundException();
            }

            return Task.FromResult(lobby);
        }

        public Task<bool> ExistsForMemberAsync(Guid userId)
            => Task.FromResult(_lobbies.Values.Any(l => l.Members.Any(m => m.PlayerId == userId)));

        public void Add(Lobby lobby) => _lobbies.Add(lobby.Id, lobby);

        public void Delete(Lobby lobby) => _lobbies.Remove(lobby.Id);
    }
}