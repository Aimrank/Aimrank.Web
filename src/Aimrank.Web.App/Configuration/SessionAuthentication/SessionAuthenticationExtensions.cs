using Microsoft.AspNetCore.Authentication;
using System;

namespace Aimrank.Web.App.Configuration.SessionAuthentication
{
    public static class SessionAuthenticationExtensions
    {
        public static AuthenticationBuilder AddSession(this AuthenticationBuilder builder)
            => AddSession(builder, _ => { });
        
        public static AuthenticationBuilder AddSession(this AuthenticationBuilder builder, Action<SessionAuthenticationOptions> configureOptions)
            => builder.AddScheme<SessionAuthenticationOptions, SessionAuthenticationHandler>(
                SessionAuthenticationDefaults.AuthenticationScheme, configureOptions);
    }
}