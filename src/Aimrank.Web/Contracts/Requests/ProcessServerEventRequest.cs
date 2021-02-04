using System;

namespace Aimrank.Web.Contracts.Requests
{
    public class ProcessServerEventRequest
    {
        public Guid ServerId { get; set; }
        public string Name { get; set; }
        public dynamic Data { get; set; }
    }
}