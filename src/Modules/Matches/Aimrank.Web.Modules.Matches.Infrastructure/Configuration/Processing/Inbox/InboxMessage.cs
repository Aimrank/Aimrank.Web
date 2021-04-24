using System;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Processing.Inbox
{
    internal class InboxMessage
    {
        public Guid Id { get; }
        public DateTime OccurredOn { get; }
        public DateTime? ProcessedDate { get; set; }
        public string Type { get; }
        public string Data { get; }
        
        private InboxMessage() {}

        public InboxMessage(Guid id, DateTime occurredOn, string type, string data)
        {
            Id = id;
            OccurredOn = occurredOn;
            Type = type;
            Data = data;
        }
    }
}