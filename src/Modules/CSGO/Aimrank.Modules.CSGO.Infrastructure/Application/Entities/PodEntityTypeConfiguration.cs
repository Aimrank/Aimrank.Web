using Aimrank.Modules.CSGO.Application.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Aimrank.Modules.CSGO.Infrastructure.Application.Entities
{
    internal class PodEntityTypeConfiguration : IEntityTypeConfiguration<Pod>
    {
        public void Configure(EntityTypeBuilder<Pod> builder)
        {
            builder.ToTable("Pods", "csgo");
            builder.HasKey(p => p.IpAddress);
        }
    }
}