using System;

namespace Aimrank.Modules.Matches.Infrastructure.Configuration.Outbox
{
    internal class OutboxMessageDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Data { get; set; }
    }
}