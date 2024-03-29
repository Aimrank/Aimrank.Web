using Aimrank.Web.Modules.Matches.Domain.Players;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Domain.Players
{
    internal class PlayerEntityTypeConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.ToTable("players");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.SteamId).IsRequired().HasMaxLength(17);

            builder.Ignore(p => p.DomainEvents);
        }
    }
}