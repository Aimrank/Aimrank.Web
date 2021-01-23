using Microsoft.EntityFrameworkCore;

namespace Aimrank.Web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Match> Matches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>(b =>
            {
                b.HasKey(m => m.Id);
                
                b.OwnsMany(b => b.Scoreboard, s =>
                {
                    s.WithOwner().HasForeignKey(p => p.MatchId);
                    s.HasKey(p => new {p.SteamId, p.MatchId});
                });
            });
        }
    }
}