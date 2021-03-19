using System.Linq;
using System.Security.Claims;
using System;

namespace Aimrank.Web.GraphQL.Subscriptions
{
    public abstract class AuthenticatedSubscriptions
    {
        protected virtual void AssertAuthenticated(ClaimsPrincipal principal, Guid? userId = null)
        {
            var isAuthenticated = principal.Identity?.IsAuthenticated ?? false;
            if (!isAuthenticated)
            {
                throw new Exception("Unauthorized");
            }

            if (userId.HasValue)
            {
                var claims = principal.Claims;
                if (claims is null)
                {
                    throw new Exception("Unauthorized");
                }

                var claim = claims.FirstOrDefault(c => c.Type == "id");
                if (claim is null || claim.Value != userId.ToString())
                {
                    throw new Exception("Unauthorized");
                }
            }
        }
        
        protected virtual Guid GetUserId(ClaimsPrincipal principal)
        {
            var claim = principal.Claims.FirstOrDefault(c => c.Type == "id");
            return claim is null ? Guid.Empty : Guid.Parse(claim.Value);
        }
    }
}