using Aimrank.Web.Modules.Cluster.Application.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Aimrank.Web.Database.Migrator")]
[assembly:InternalsVisibleTo("Aimrank.Web.App")]

namespace Aimrank.Web.Modules.Cluster.Infrastructure
{
    internal class ClusterContext : DbContext
    {
        public DbSet<Pod> Pods { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<SteamToken> SteamTokens { get; set; }
        
        public ClusterContext(DbContextOptions<ClusterContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}