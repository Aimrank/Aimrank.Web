using Aimrank.Domain.Lobbies;
using Aimrank.Domain.Matches;
using Aimrank.Domain.RefreshTokens;
using Aimrank.Domain.Users;
using Aimrank.Infrastructure.Configuration.Outbox;
using Aimrank.Infrastructure.Domain.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Aimrank.Database.Migrator")]
[assembly:InternalsVisibleTo("Aimrank.Web")]
[assembly:InternalsVisibleTo("Aimrank.IntegrationTests")]

namespace Aimrank.Infrastructure
{
    internal class AimrankContext : IdentityDbContext<UserModel, IdentityRole<UserId>, UserId>
    {
        public DbSet<Match> Matches { get; set; }
        public DbSet<Lobby> Lobbies { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }
        
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