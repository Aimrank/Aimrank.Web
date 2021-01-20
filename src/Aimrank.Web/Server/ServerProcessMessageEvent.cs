using System;

namespace Aimrank.Web.Server
{
    public class ServerProcessMessageEvent : EventArgs
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