using Aimrank.Web.Modules.Matches.Domain.Lobbies;
using Aimrank.Web.Modules.Matches.Domain.Matches;
using Aimrank.Web.Modules.Matches.Domain.Players;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Processing.Inbox;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Processing.Outbox;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Aimrank.Web.Database.Migrator")]
[assembly:InternalsVisibleTo("Aimrank.Web.App")]
[assembly:InternalsVisibleTo("Aimrank.Web.Modules.Matches.ArchTests")]

namespace Aimrank.Web.Modules.Matches.Infrastructure
{
    internal class MatchesContext : DbContext
    {
        public DbSet<Match> Matches { get; set; }
        public DbSet<Lobby> Lobbies { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<InboxMessage> InboxMessages { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }
        
        public MatchesContext(DbContextOptions<MatchesContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("matches");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}