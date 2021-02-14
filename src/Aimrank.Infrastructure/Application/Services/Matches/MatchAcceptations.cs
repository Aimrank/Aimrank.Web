using System.Collections.Generic;
using System;

namespace Aimrank.Infrastructure.Application.Services.Matches
{
    internal class MatchAcceptations
    {
        private readonly HashSet<Guid> _users;
        private readonly HashSet<Guid> _usersAccepted;

        public IEnumerable<Guid> Users => _users;

        public IEnumerable<Guid> UsersAccepted => _usersAccepted;

        public MatchAcceptations(
            IEnumerable<Guid> users,
            IEnumerable<Guid> usersAccepted)
        {
            _users = new HashSet<Guid>(users);
            _usersAccepted = new HashSet<Guid>(usersAccepted);
        }

        public void Accept(Guid userId)
        {
            if (_usersAccepted.Contains(userId))
            {
                return;
            }
            
            if (!_users.Contains(userId))
            {
                throw new MatchAcceptationException();
            }

            _usersAccepted.Add(userId);
        }

        public bool IsAccepted() => _users.Count == _usersAccepted.Count;
    }
}