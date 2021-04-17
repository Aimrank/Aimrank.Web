using System;

namespace Aimrank.Common.Application.Events
{
    public class IntegrationEventAttribute : Attribute
    {
        public string Service { get; }

        public IntegrationEventAttribute(string service)
        {
            Service = service;
        }
    }
}