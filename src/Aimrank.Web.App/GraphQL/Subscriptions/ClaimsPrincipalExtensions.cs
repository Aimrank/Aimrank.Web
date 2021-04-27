using System.Linq;
using System.Security.Claims;
using System;

namespace Aimrank.Web.App.GraphQL.Subscriptions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            var claim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return claim is null ? Guid.Empty : Guid.Parse(claim.Value);
        }
    }
}