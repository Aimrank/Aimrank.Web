using System;

namespace Aimrank.Application.Events
{
    public class ServerMessageReceivedEvent : IntegrationEvent
    {
        public Guid ServerId { get; }
        public string Content { get; }

        public ServerMessageReceivedEvent(
            Guid id,
            Guid serverId, 
            string content,
            DateTime occurredAt)
            : base(id, occurredAt)
        {
            ServerId = serverId;
            Content = content;
        }
    }
}