using Aimrank.Domain.Lobbies;
using Aimrank.Domain.Matches;
using Aimrank.Infrastructure.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Aimrank.Infrastructure.Domain.Lobbies
{
    internal class LobbyEntityTypeConfiguration : IEntityTypeConfiguration<Lobby>
    {
        public void Configure(EntityTypeBuilder<Lobby> builder)
        {
            builder.ToTable("Lobbies", "aimrank");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Id).HasColumnName("Id");
            builder.Property(l => l.MatchId).HasColumnName("MatchId");
            builder.Property(l => l.Status).HasColumnName("Status");
            
            builder.OwnsOne(l => l.Configuration, b =>
            {
                b.Property(c => c.Map).HasColumnName("Configuration_Map").IsRequired().HasMaxLength(50);
                b.Property(c => c.Name).HasColumnName("Configuration_Name").IsRequired().HasMaxLength(450);
                b.Property(c => c.Mode).HasColumnName("Configuration_Mode");
            });

            builder.OwnsMany<LobbyMember>("_members", b =>
            {
                b.ToTable("LobbiesMembers", "aimrank");
                b.HasKey(m => m.UserId);
                b.Property(m => m.UserId).HasColumnName("UserId").IsRequired();
                b.Property(m => m.Role).HasColumnName("Role");
                b.HasOne<UserModel>().WithOne().HasForeignKey<LobbyMember>("UserId");
                b.WithOwner();
            });

            builder.OwnsMany<LobbyInvitation>("_invitations", b =>
            {
                b.ToTable("LobbiesInvitations", "aimrank");
                b.Property<LobbyId>("LobbyId");
                b.Property(i => i.InvitingUserId).IsRequired().HasColumnName("InvitingUserId");
                b.Property(i => i.InvitedUserId).IsRequired().HasColumnName("InvitedUserId");
                b.Property(i => i.CreatedAt).HasColumnName("CreatedAt");
                b.HasKey("LobbyId", "InvitingUserId", "InvitedUserId");
                b.HasOne<UserModel>().WithMany().HasForeignKey("InvitingUserId");
                b.HasOne<UserModel>().WithMany().HasForeignKey("InvitedUserId");
                b.WithOwner();
            });
            
            builder.HasOne<Match>().WithOne().HasForeignKey<Lobby>(l => l.MatchId);

            builder.Ignore(l => l.Members);
            builder.Ignore(l => l.Invitations);
            builder.Ignore(l => l.DomainEvents);
        }
    }
}