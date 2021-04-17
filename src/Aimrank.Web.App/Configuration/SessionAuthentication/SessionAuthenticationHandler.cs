using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace Aimrank.Web.App.Configuration.SessionAuthentication
{
    public class SessionAuthenticationHandler : AuthenticationHandler<SessionAuthenticationOptions>,
        IAuthenticationSignInHandler
    {
        private ISession Session => Request.HttpContext.Session;
        
        public SessionAuthenticationHandler(
            IOptionsMonitor<SessionAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Session.IsAvailable)
            {
                return AuthenticateResult.NoResult();
            }

            var claims = await GetClaimsFromSessionAsync();
            if (claims.Count == 0)
            {
                return AuthenticateResult.NoResult();
            }

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            
            return AuthenticateResult.Success(ticket);
        }

        public async Task SignOutAsync(AuthenticationProperties properties)
        {
            await Session.LoadAsync();
            
            Session.Clear();
            
            await Session.CommitAsync();
        }

        public async Task SignInAsync(ClaimsPrincipal user, AuthenticationProperties properties)
        {
            await Session.LoadAsync();
            
            var sessionClaims = user.Claims.Select(c => new SessionClaim(c.Type, c.Value)).ToList();
            var sessionClaimsJson = JsonSerializer.Serialize(sessionClaims);
            
            Session.SetString("claims", sessionClaimsJson);
            
            await Session.CommitAsync();
        }

        private async Task<List<Claim>> GetClaimsFromSessionAsync()
        {
            await Session.LoadAsync();
            
            var sessionClaimsJson = Session.GetString("claims");
            if (sessionClaimsJson is null)
            {
                return new List<Claim>();
            }

            var sessionClaims = JsonSerializer.Deserialize<List<SessionClaim>>(sessionClaimsJson);

            return sessionClaims is null
                ? new List<Claim>()
                : sessionClaims.Select(c => new Claim(c.Type, c.Value)).ToList();
        }
        
        private record SessionClaim(string Type, string Value);
    }
}