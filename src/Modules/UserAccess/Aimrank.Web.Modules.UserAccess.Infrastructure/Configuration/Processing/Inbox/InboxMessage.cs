using System;

namespace Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.Processing.Inbox
{
    internal class InboxMessage
    {
        public Guid Id { get; }
        public DateTime OccurredAt { get; }
        public DateTime ProcessedDate { get; }
        public string Type { get; }
        public string Data { get; }
        
        private InboxMessage() {}

        public InboxMessage(Guid id, DateTime occurredAt, string type, string data)
        {
            Id = id;
            OccurredAt = occurredAt;
            Type = type;
            Data = data;
        }
    }
}