using System;

namespace Aimrank.Web.Server
{
    public class ServerProcessLogEvent : EventArgs
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