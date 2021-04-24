using Aimrank.Web.Modules.Matches.Domain.Lobbies;
using Aimrank.Web.Modules.Matches.Domain.Players;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Domain.Lobbies
{
    internal class LobbyEntityTypeConfiguration : IEntityTypeConfiguration<Lobby>
    {
        public void Configure(EntityTypeBuilder<Lobby> builder)
        {
            builder.ToTable("lobbies");

            builder.HasKey(l => l.Id);

            builder.OwnsOne(l => l.Configuration, b =>
                {
                    b.Property<string>("_maps").HasColumnName("configuration_maps").IsRequired();
                    b.Property(c => c.Name).HasColumnName("configuration_name").IsRequired().HasMaxLength(450);
                    b.Property(c => c.Mode).HasColumnName("configuration_mode");
                    b.Ignore(c => c.Maps);
                })
                .Navigation(l => l.Configuration).IsRequired();

            builder.OwnsMany(l => l.Members, b =>
            {
                b.ToTable("lobbies_members");
                b.HasKey(m => m.PlayerId);
                b.Property(m => m.PlayerId).HasColumnName("player_id").IsRequired();
                b.Property(m => m.Role).HasColumnName("role");
                b.HasOne<Player>().WithOne().HasForeignKey<LobbyMember>(m => m.PlayerId);
                b.WithOwner();
            });

            builder.OwnsMany(l => l.Invitations, b =>
            {
                b.ToTable("lobbies_invitations");
                b.Property<LobbyId>("LobbyId");
                b.Property(i => i.InvitingPlayerId).HasColumnName("inviting_player_id");
                b.Property(i => i.InvitedPlayerId).HasColumnName("invited_player_id").IsRequired();
                b.Property(i => i.CreatedAt).HasColumnName("created_at");
                b.HasKey("LobbyId", "InvitingPlayerId", "InvitedPlayerId");
                b.HasOne<Player>().WithMany().HasForeignKey(i => i.InvitedPlayerId).OnDelete(DeleteBehavior.Restrict);
                b.HasOne<Player>().WithMany().HasForeignKey(i => i.InvitingPlayerId).OnDelete(DeleteBehavior.Restrict);
                b.WithOwner();
            });
            
            builder.Ignore(l => l.DomainEvents);
        }
    }
}