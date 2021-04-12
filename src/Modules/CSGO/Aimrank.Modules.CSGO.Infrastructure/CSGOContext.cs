using Aimrank.Modules.CSGO.Application.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Aimrank.Database.Migrator")]
[assembly:InternalsVisibleTo("Aimrank.Web")]

namespace Aimrank.Modules.CSGO.Infrastructure
{
    internal class CSGOContext : DbContext
    {
        public DbSet<Pod> Pods { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<SteamToken> SteamKeys { get; set; }
        
        public CSGOContext(DbContextOptions<CSGOContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}