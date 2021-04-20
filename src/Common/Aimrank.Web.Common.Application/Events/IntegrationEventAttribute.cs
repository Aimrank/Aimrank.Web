using System;

namespace Aimrank.Web.Common.Application.Events
{
    public class IntegrationEventAttribute : Attribute
    {
        public string Service { get; }
        public bool Outbound { get; }

        public IntegrationEventAttribute(string service = null, bool outbound = false)
        {
            Service = service;
            Outbound = outbound;
        }
    }
}