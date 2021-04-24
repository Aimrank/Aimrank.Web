using MediatR;
using System;

namespace Aimrank.Web.Common.Application.Events
{
    public interface IIntegrationEvent : INotification
    {
        public Guid Id { get; }
        public DateTime OccurredOn { get; }
    }
}