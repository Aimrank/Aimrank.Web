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

            builder.HasOne<Match>().WithOne().HasForeignKey<Lobby>(l => l.MatchId);

            builder.OwnsMany(l => l.Members, b =>
            {
                b.ToTable("LobbiesMembers", "aimrank");
                b.HasKey(m => m.UserId);
                b.HasOne<UserModel>().WithOne().HasForeignKey<LobbyMember>("UserId");
                b.WithOwner();
            });

            builder.OwnsOne(l => l.Configuration, b =>
            {
                b.Property(c => c.Map).HasMaxLength(50);
            });
        }
    }
}