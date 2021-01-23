using Aimrank.Web.Data;
using Aimrank.Web.Events;
using Aimrank.Web.Hubs;
using Aimrank.Web.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Aimrank.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        
        public Startup(IConfiguration configuration) => _configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("Database")));

            services.AddSingleton<EventBus>();
            
            services.AddSignalR();
            services.AddSingleton<ServerProcessManager>();
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<GameHub>("/hubs/game");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}