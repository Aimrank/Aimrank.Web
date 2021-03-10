using System;

namespace Aimrank.Web.Contracts
{
    public class ProcessServerEventRequest
    {
        public Guid MatchId { get; set; }
        public string Name { get; set; }
        public dynamic Data { get; set; }
    }
}