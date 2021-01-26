using System;

namespace Aimrank.Infrastructure.Application.CSGO
{
    internal class ServerProcessLogEvent : EventArgs
    {
        public Guid Id { get; }
        public string Content { get; }

        public ServerProcessLogEvent(Guid id, string content)
        {
            Id = id;
            Content = content;
        }
    }
}