using System;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Processing.Outbox
{
    internal class OutboxMessage
    {
        public Guid Id { get; }
        public DateTime OccurredAt { get; }
        public DateTime? ProcessedDate { get; set; }
        public string Type { get; }
        public string Data { get; }

        private OutboxMessage() {}

        public OutboxMessage(Guid id, DateTime occurredAt, string type, string data)
        {
            Id = id;
            OccurredAt = occurredAt;
            Type = type;
            Data = data;
        }
    }
}