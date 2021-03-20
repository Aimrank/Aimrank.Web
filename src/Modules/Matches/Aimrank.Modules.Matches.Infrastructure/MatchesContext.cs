using Aimrank.Modules.Matches.Domain.Lobbies;
using Aimrank.Modules.Matches.Domain.Matches;
using Aimrank.Modules.Matches.Domain.Players;
using Aimrank.Modules.Matches.Infrastructure.Configuration.Outbox;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Aimrank.Database.Migrator")]
[assembly:InternalsVisibleTo("Aimrank.Web")]
[assembly:InternalsVisibleTo("Aimrank.Modules.Matches.IntegrationTests")]
[assembly:InternalsVisibleTo("Aimrank.Modules.Matches.ArchTests")]

namespace Aimrank.Modules.Matches.Infrastructure
{
    internal class MatchesContext : DbContext
    {
        public DbSet<Match> Matches { get; set; }
        public DbSet<Lobby> Lobbies { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }
        
        public MatchesContext(DbContextOptions<MatchesContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}