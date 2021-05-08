using Aimrank.Web.Modules.UserAccess.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.Emails
{
    internal static class Extensions
    {
        public static IServiceCollection AddEmails(this IServiceCollection services, EmailSettings emailSettings)
        {
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddSingleton(emailSettings);

            return services;
        }
    }
}