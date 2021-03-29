using System.Collections.Generic;
using Aimrank.Common.Domain;

namespace Aimrank.Common.Infrastructure
{
    public interface IDomainEventAccessor
    {
        IEnumerable<IDomainEvent> GetDomainEvents();
    }
}