using System.Collections.Generic;
using Aimrank.Web.Common.Domain;

namespace Aimrank.Web.Common.Infrastructure
{
    public interface IDomainEventAccessor
    {
        IEnumerable<IDomainEvent> GetDomainEvents();
    }
}