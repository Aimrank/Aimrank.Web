using Aimrank.Web.Common.Application;
using Aimrank.Web.Common.Infrastructure;
using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using Aimrank.Web.Modules.UserAccess.Application.Services;
using Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.DataAccess;
using Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.Emails;
using Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.Processing;
using Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.Quartz;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration
{
    public class UserAccessModuleStartup : IModuleStartup
    {
        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IUserAccessModule, UserAccessModule>();
            services.AddScoped<DbContext, UserAccessContext>();
            services.AddDbContext<UserAccessContext>(options => options
                .UseNpgsql(configuration.GetConnectionString("Database"),
                    x => x.MigrationsAssembly(GetType().Assembly.FullName))
                .UseSnakeCaseNamingConvention()
                .ReplaceService<IValueConverterSelector, EntityIdValueConverterSelector>());
        }
        
        public void Initialize(IApplicationBuilder builder, IConfiguration configuration)
        {
            var settings = configuration.GetSection(nameof(UserAccessModuleSettings)).Get<UserAccessModuleSettings>();
            
            ILogger logger = builder.ApplicationServices.GetRequiredService<ILogger<UserAccessModule>>();
            
            var services = new ServiceCollection();

            services.AddDataAccess(configuration.GetConnectionString("Database"));
            services.AddProcessing();
            services.AddEmails(settings.EmailSettings);
            services.AddQuartz();
            services.AddSingleton(builder.ApplicationServices.GetRequiredService<IExecutionContextAccessor>());
            services.AddSingleton(builder.ApplicationServices.GetRequiredService<IUrlFactory>());
            services.AddSingleton(logger);
            
            UserAccessCompositionRoot.SetContainer(services.BuildServiceProvider());
            
            QuartzStartup.Initialize();
        }
    }
}