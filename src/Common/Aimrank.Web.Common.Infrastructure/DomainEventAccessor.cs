using Aimrank.Web.Common.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Aimrank.Web.Common.Infrastructure
{
    public class DomainEventAccessor : IDomainEventAccessor
    {
        private readonly DbContext _context;

        public DomainEventAccessor(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<IDomainEvent> GetDomainEvents()
        {
            var domainEntities = _context.ChangeTracker
                .Entries<Entity>()
                .Where(e => e.Entity.DomainEvents.Any())
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(e => e.Entity.DomainEvents)
                .ToList();

            foreach (var entity in domainEntities)
            {
                entity.Entity.ClearDomainEvents();
            }

            return domainEvents;
        }
    }
}