using Aimrank.Domain.Matches;
using Aimrank.Domain.RefreshTokens;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Aimrank.Database.Migrator")]
[assembly:InternalsVisibleTo("Aimrank.Web")]

namespace Aimrank.Infrastructure
{
    internal class AimrankContext : IdentityDbContext
    {
        public DbSet<Match> Matches { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        
        public AimrankContext(DbContextOptions<AimrankContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("aimrank");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}