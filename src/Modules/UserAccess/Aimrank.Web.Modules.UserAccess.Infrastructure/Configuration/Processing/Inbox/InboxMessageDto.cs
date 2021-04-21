using System;

namespace Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.Processing.Inbox
{
    internal class InboxMessageDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Data { get; set; }
    }
}