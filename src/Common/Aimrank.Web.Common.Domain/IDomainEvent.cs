using MediatR;
using System;

namespace Aimrank.Web.Common.Domain
{
    public interface IDomainEvent : INotification
    {
        public Guid Id { get; }
        public DateTime OccurredOn { get; }
    }
}