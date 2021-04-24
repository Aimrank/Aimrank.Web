using Aimrank.Web.Modules.Cluster.Application.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Aimrank.Web.Modules.Cluster.Infrastructure.Application.Entities
{
    internal class PodEntityTypeConfiguration : IEntityTypeConfiguration<Pod>
    {
        public void Configure(EntityTypeBuilder<Pod> builder)
        {
            builder.ToTable("Pods");
            builder.HasKey(p => p.IpAddress);
        }
    }
}