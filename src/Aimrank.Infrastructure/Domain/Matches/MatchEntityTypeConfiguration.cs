using Aimrank.Domain.Matches;
using Aimrank.Infrastructure.Domain.Users;
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
            
            builder.Property(m => m.Map).IsRequired().HasMaxLength(50);
            builder.Property(m => m.Address).HasMaxLength(50);
            
            builder.OwnsMany(m => m.Players, b =>
            {
                b.ToTable("MatchesPlayers", "aimrank");
                b.Property<MatchId>("MatchId").IsRequired();
                b.Property(p => p.SteamId).IsRequired().HasMaxLength(17);
                b.HasOne<UserModel>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Restrict);
                b.WithOwner().HasForeignKey("MatchId");
                b.HasKey("MatchId", "UserId");
            });
        }
    }
}