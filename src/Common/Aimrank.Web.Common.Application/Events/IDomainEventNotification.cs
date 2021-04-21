using MediatR;
using System;

namespace Aimrank.Web.Common.Application.Events
{
    public interface IDomainEventNotification<out TEvent> : IDomainEventNotification
    {
        TEvent DomainEvent { get; }
    }

    public interface IDomainEventNotification : INotification
    {
        Guid Id { get; }
    }
}