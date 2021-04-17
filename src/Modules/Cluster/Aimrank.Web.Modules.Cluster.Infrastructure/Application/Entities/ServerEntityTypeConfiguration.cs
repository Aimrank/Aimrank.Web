using Aimrank.Web.Modules.Cluster.Application.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Aimrank.Web.Modules.Cluster.Infrastructure.Application.Entities
{
    internal class ServerEntityTypeConfiguration : IEntityTypeConfiguration<Server>
    {
        public void Configure(EntityTypeBuilder<Server> builder)
        {
            builder.ToTable("Servers", "csgo");
            builder.HasKey(s => s.MatchId);
            
            builder
                .HasOne(s => s.SteamToken)
                .WithOne(t => t.Server)
                .HasForeignKey<Server>();
                
            builder.Navigation(s => s.Pod).IsRequired();
            builder.Navigation(s => s.SteamToken).IsRequired();
        }
    }
}