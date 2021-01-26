using System;

namespace Aimrank.Infrastructure.Application.CSGO
{
    internal class ServerProcessMessageEvent : EventArgs
    {
        public Guid Id { get; }
        public string Content { get; }

        public ServerProcessMessageEvent(Guid id, string content)
        {
            Id = id;
            Content = content;
        }
    }
}