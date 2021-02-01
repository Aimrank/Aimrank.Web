using Aimrank.Domain.Matches;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;

namespace Aimrank.Infrastructure.Domain.Matches
{
    internal class MatchEntityTypeConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.ToTable("Matches", "aimrank");
            
            builder.HasKey(m => m.Id);
            
            builder.Property(m => m.Map).IsRequired().HasMaxLength(50);
            
            builder.OwnsMany(m => m.Players, b =>
            {
                b.ToTable("MatchesPlayers", "aimrank");
                b.Property<Guid>("MatchId").IsRequired();
                b.Property(p => p.SteamId).IsRequired().HasMaxLength(17);
                b.WithOwner().HasForeignKey("MatchId");
                b.HasKey("MatchId", "UserId");
            });
        }
    }
}