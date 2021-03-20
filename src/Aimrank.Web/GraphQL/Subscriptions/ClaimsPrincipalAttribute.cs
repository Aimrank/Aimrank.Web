using HotChocolate;
using System.Security.Claims;
using System;

namespace Aimrank.Web.GraphQL.Subscriptions
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class ClaimsPrincipalAttribute : GlobalStateAttribute
    {
        public ClaimsPrincipalAttribute() : base(nameof(ClaimsPrincipal))
        {
        }
    }
}