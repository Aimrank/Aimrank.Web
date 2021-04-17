using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace Aimrank.Web.App.Steam
{
    public static class SteamHttpContextExtensions
    {
        public static SteamDataDto GetSteamData(this HttpContext httpContext)
        {
            var steamIdUrl = GetClaim(httpContext, ClaimTypes.NameIdentifier)?.Value;
            
            return new SteamDataDto
            {
                Id = steamIdUrl?.Substring(steamIdUrl.LastIndexOf('/') + 1)
            };
        }

        private static Claim GetClaim(HttpContext httpContext, string name)
            => httpContext.User.Claims.FirstOrDefault(c => c.Type == name);
    }
}