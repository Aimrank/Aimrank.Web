using Aimrank.Web.Modules.Cluster.Application.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Aimrank.Web.Modules.Cluster.Infrastructure.Application.Entities
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