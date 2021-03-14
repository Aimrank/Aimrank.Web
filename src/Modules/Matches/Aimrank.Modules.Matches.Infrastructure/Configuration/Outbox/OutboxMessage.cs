using System;

namespace Aimrank.Modules.Matches.Infrastructure.Configuration.Outbox
{
    internal class OutboxMessage
    {
        public Guid Id { get; }
        public DateTime OccurredAt { get; }
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