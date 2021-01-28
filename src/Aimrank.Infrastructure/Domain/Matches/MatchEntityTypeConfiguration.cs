using Aimrank.Domain.Matches;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Aimrank.Infrastructure.Domain.Matches
{
    internal class MatchEntityTypeConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.ToTable("Matches", "aimrank");
            
            builder.HasKey(m => m.Id);
            
            builder.OwnsMany(m => m.Players, b =>
            {
                b.ToTable("MatchesPlayers", "aimrank");
                b.HasKey(p => new {p.SteamId, p.MatchId});
                b.WithOwner().HasForeignKey(p => p.MatchId);
                b.Property(p => p.Name).HasMaxLength(32);
            });
        }
    }
}