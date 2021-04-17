using Aimrank.Web.Modules.CSGO.Application.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Aimrank.Web.Database.Migrator")]
[assembly:InternalsVisibleTo("Aimrank.Web.App")]

namespace Aimrank.Web.Modules.CSGO.Infrastructure
{
    internal class CSGOContext : DbContext
    {
        public DbSet<Pod> Pods { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<SteamToken> SteamTokens { get; set; }
        
        public CSGOContext(DbContextOptions<CSGOContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}