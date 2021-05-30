using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Aimrank.Web.App.Configuration.Controllers
{
    public static class Extensions
    {
        public static IServiceCollection AddApplicationControllers(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddControllersWithViews()
                .ConfigureApplicationPartManager(manager =>
                {
                    var controllerProvider = manager.FeatureProviders.FirstOrDefault(
                        provider => provider.GetType() == typeof(ControllerFeatureProvider));
                        
                    if (controllerProvider is not null)
                    {
                        manager.FeatureProviders.Remove(controllerProvider);
                    }
                        
                    manager.FeatureProviders.Add(new DevelopmentControllerFeatureProvider(configuration));
                });
            
            services.AddRouting(options => options.LowercaseUrls = true);
                
            return services;
        }
    }
}