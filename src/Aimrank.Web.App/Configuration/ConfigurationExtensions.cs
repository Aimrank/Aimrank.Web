using Microsoft.Extensions.Configuration;
using System;

namespace Aimrank.Web.App.Configuration
{
    public static class ConfigurationExtensions
    {
        public static bool IsDevelopment(this IConfiguration configuration) =>
            configuration.GetSection("ASPNETCORE_ENVIRONMENT").Get<string>()
                .Equals("Development", StringComparison.InvariantCultureIgnoreCase);
    }
}