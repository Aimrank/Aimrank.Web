using Aimrank.Modules.Matches.Domain.Lobbies;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Aimrank.Modules.Matches.Infrastructure.Domain.Lobbies
{
    internal class LobbyEntityTypeConfiguration : IEntityTypeConfiguration<Lobby>
    {
        public void Configure(EntityTypeBuilder<Lobby> builder)
        {
            builder.ToTable("Lobbies", "matches");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Id).HasColumnName("Id");
            builder.Property(l => l.Status).HasColumnName("Status");

            builder.OwnsOne<LobbyConfiguration>("_configuration", b =>
                {
                    b.Property(c => c.Map).HasColumnName("Configuration_Map").IsRequired().HasMaxLength(50);
                    b.Property(c => c.Name).HasColumnName("Configuration_Name").IsRequired().HasMaxLength(450);
                    b.Property(c => c.Mode).HasColumnName("Configuration_Mode");
                })
                .Navigation("_configuration").IsRequired();

            builder.OwnsMany(l => l.Members, b =>
            {
                b.ToTable("LobbiesMembers", "matches");
                b.HasKey(m => m.UserId);
                b.Property(m => m.UserId).HasColumnName("UserId").IsRequired();
                b.Property(m => m.Role).HasColumnName("Role");
                b.WithOwner();
            });

            builder.OwnsMany(l => l.Invitations, b =>
            {
                b.ToTable("LobbiesInvitations", "matches");
                b.Property<LobbyId>("LobbyId");
                b.Property(i => i.InvitingUserId).HasColumnName("InvitingUserId");
                b.Property(i => i.InvitedUserId).HasColumnName("InvitedUserId").IsRequired();
                b.Property(i => i.CreatedAt).HasColumnName("CreatedAt");
                b.HasKey("LobbyId", "InvitingUserId", "InvitedUserId");
                b.WithOwner();
            });
            
            builder.Ignore(l => l.DomainEvents);
        }
    }
}