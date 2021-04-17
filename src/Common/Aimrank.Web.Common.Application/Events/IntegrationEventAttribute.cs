using System;

namespace Aimrank.Web.Common.Application.Events
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