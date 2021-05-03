using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aimrank.Web.App
{
    public static class HostExtensions
    {
        public static async Task MigrateDatabaseAsync(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            
            var contexts = scope.ServiceProvider.GetRequiredService<IEnumerable<DbContext>>();

            foreach (var context in contexts)
            {
                var migrations = await context.Database.GetPendingMigrationsAsync();
                if (migrations.Any())
                {
                    await context.Database.MigrateAsync();
                }
            }
        }
    }
}