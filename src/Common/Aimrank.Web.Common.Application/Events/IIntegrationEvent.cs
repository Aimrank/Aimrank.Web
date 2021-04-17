using System;

namespace Aimrank.Web.Common.Application.Events
{
    public interface IIntegrationEvent
    {
        public Guid Id { get; }
        public DateTime OccurredAt { get; }
    }
}