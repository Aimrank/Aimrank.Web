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
            builder.ToTable("Lobbies");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Id).HasColumnName("Id");
            builder.Property(l => l.Status).HasColumnName("Status");

            builder.OwnsOne(l => l.Configuration, b =>
                {
                    b.Property<string>("_maps").HasColumnName("Configuration_Maps").IsRequired();
                    b.Property(c => c.Name).HasColumnName("Configuration_Name").IsRequired().HasMaxLength(450);
                    b.Property(c => c.Mode).HasColumnName("Configuration_Mode");
                    b.Ignore(c => c.Maps);
                })
                .Navigation(l => l.Configuration).IsRequired();

            builder.OwnsMany(l => l.Members, b =>
            {
                b.ToTable("LobbiesMembers");
                b.HasKey(m => m.PlayerId);
                b.Property(m => m.PlayerId).HasColumnName("PlayerId").IsRequired();
                b.Property(m => m.Role).HasColumnName("Role");
                b.HasOne<Player>().WithOne().HasForeignKey<LobbyMember>(m => m.PlayerId);
                b.WithOwner();
            });

            builder.OwnsMany(l => l.Invitations, b =>
            {
                b.ToTable("LobbiesInvitations");
                b.Property<LobbyId>("LobbyId");
                b.Property(i => i.InvitingPlayerId).HasColumnName("InvitingPlayerId");
                b.Property(i => i.InvitedPlayerId).HasColumnName("InvitedPlayerId").IsRequired();
                b.Property(i => i.CreatedAt).HasColumnName("CreatedAt");
                b.HasKey("LobbyId", "InvitingPlayerId", "InvitedPlayerId");
                b.HasOne<Player>().WithMany().HasForeignKey(i => i.InvitedPlayerId).OnDelete(DeleteBehavior.Restrict);
                b.HasOne<Player>().WithMany().HasForeignKey(i => i.InvitingPlayerId).OnDelete(DeleteBehavior.Restrict);
                b.WithOwner();
            });
            
            builder.Ignore(l => l.DomainEvents);
        }
    }
}