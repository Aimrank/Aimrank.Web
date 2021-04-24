using System;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Processing.Outbox
{
    internal class OutboxMessage
    {
        public Guid Id { get; }
        public DateTime OccurredOn { get; }
        public DateTime? ProcessedDate { get; set; }
        public string Type { get; }
        public string Data { get; }

        private OutboxMessage() {}

        public OutboxMessage(Guid id, DateTime occurredOn, string type, string data)
        {
            Id = id;
            OccurredOn = occurredOn;
            Type = type;
            Data = data;
        }
    }
}