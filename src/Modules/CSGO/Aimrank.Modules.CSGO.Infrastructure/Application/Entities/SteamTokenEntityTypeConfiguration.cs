using Aimrank.Modules.CSGO.Application.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Aimrank.Modules.CSGO.Infrastructure.Application.Entities
{
    internal class SteamTokenEntityTypeConfiguration : IEntityTypeConfiguration<SteamToken>
    {
        public void Configure(EntityTypeBuilder<SteamToken> builder)
        {
            builder.ToTable("SteamTokens", "csgo");
            builder.HasKey(t => t.Token);
        }
    }
}