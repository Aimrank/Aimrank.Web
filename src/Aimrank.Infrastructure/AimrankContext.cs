using Aimrank.Domain.Matches;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Aimrank.Database.Migrator")]

namespace Aimrank.Infrastructure
{
    internal class AimrankContext : DbContext
    {
        public DbSet<Match> Matches { get; set; }
        
        public AimrankContext(DbContextOptions<AimrankContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}