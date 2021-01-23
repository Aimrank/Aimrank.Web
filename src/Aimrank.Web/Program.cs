using Aimrank.Web.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Aimrank.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webHost = CreateHostBuilder(args).Build();

            using (var scope = webHost.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DataContext>();
                context.Database.Migrate();
            }
            
            webHost.Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}