using System;

namespace Aimrank.Web.Server
{
    public class ServerProcessStatusChangedEvent : EventArgs
    {
        public Guid Id { get; }
        public ServerProcessStatus Status { get; }

        public ServerProcessStatusChangedEvent(Guid id, ServerProcessStatus status)
        {
            Id = id;
            Status = status;
        }
    }
}