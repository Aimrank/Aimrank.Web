using Aimrank.Domain.Lobbies;
using Aimrank.Domain.Matches;
using Aimrank.Infrastructure.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Aimrank.Infrastructure.Domain.Matches
{
    internal class MatchEntityTypeConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.ToTable("Matches", "aimrank");
            
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id).HasColumnName("Id");
            builder.Property(m => m.ScoreT).HasColumnName("ScoreT");
            builder.Property(m => m.ScoreCT).HasColumnName("ScoreCT");
            builder.Property(m => m.Map).HasColumnName("Map").IsRequired().HasMaxLength(50);
            builder.Property(m => m.Address).HasColumnName("Address").HasMaxLength(50);
            builder.Property(m => m.CreatedAt).HasColumnName("CreatedAt");
            builder.Property(m => m.FinishedAt).HasColumnName("FinishedAt");
            builder.Property(m => m.Status).HasColumnName("Status");
            
            builder.OwnsMany(m => m.Players, b =>
            {
                b.ToTable("MatchesPlayers", "aimrank");
                b.Property<MatchId>("MatchId");
                b.Property(p => p.UserId).HasColumnName("UserId").IsRequired();
                b.Property(p => p.SteamId).HasColumnName("SteamId").IsRequired().HasMaxLength(17);
                b.Property(p => p.Team).HasColumnName("Team");
                b.Property(p => p.Kills).HasColumnName("Kills");
                b.Property(p => p.Assists).HasColumnName("Assists");
                b.Property(p => p.Deaths).HasColumnName("Deaths");
                b.Property(p => p.Score).HasColumnName("Score");
                b.HasKey("MatchId", "UserId");
                b.HasOne<UserModel>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Restrict);
                b.WithOwner().HasForeignKey("MatchId");
            });

            builder.OwnsMany(m => m.Lobbies, b =>
            {
                b.ToTable("MatchesLobbies", "aimrank");
                b.Property<MatchId>("MatchId");
                b.Property<LobbyId>("LobbyId");
                b.HasKey("MatchId", "LobbyId");
                b.HasOne<Lobby>().WithOne().HasForeignKey<MatchLobby>("LobbyId");
                b.WithOwner().HasForeignKey("MatchId");
            });

            builder.Ignore(m => m.DomainEvents);
        }
    }
}