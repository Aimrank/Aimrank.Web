using Aimrank.Web.Modules.Matches.Domain.Players;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Domain.Players
{
    internal class PlayerEntityTypeConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.ToTable("Players", "matches");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.SteamId).HasColumnName("SteamId").IsRequired().HasMaxLength(17);

            builder.Ignore(p => p.DomainEvents);
        }
    }
}