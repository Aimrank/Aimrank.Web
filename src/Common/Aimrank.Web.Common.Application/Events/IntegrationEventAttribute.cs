using System;

namespace Aimrank.Web.Common.Application.Events
{
    public class IntegrationEventAttribute : Attribute
    {
        public string Service { get; }
        public IntegrationEventType Type { get; }

        public IntegrationEventAttribute(
            string service = null,
            IntegrationEventType type = IntegrationEventType.Inbound)
        {
            Service = service;
            Type = type;
        }
    }

    public enum IntegrationEventType
    {
        Inbound,
        Outbound
    }
}