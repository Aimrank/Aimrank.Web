using Aimrank.Web.Modules.Matches.Domain.Lobbies;
using Aimrank.Web.Modules.Matches.Domain.Matches;
using Aimrank.Web.Modules.Matches.Domain.Players;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Domain.Matches
{
    internal class MatchEntityTypeConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.ToTable("Matches");
            
            builder.HasKey(m => m.Id);

            builder.Property<MatchWinner>("_winner").HasColumnName("Winner");
            builder.Property<int>("_scoreT").HasColumnName("ScoreT");
            builder.Property<int>("_scoreCT").HasColumnName("ScoreCT");
            builder.Property<string>("_address").HasColumnName("Address").HasMaxLength(50);
            builder.Property<DateTime>("_createdAt").HasColumnName("CreatedAt");
            builder.Property(m => m.Map).HasColumnName("Map").IsRequired().HasMaxLength(50);
            builder.Property(m => m.Mode).HasColumnName("Mode");
            builder.Property(m => m.FinishedAt).HasColumnName("FinishedAt");
            builder.Property(m => m.Status).HasColumnName("Status");
            
            builder.OwnsMany(m => m.Players, b =>
            {
                b.ToTable("MatchesPlayers");
                b.Property<MatchId>("MatchId");
                b.Property(p => p.PlayerId).HasColumnName("PlayerId").IsRequired();
                b.Property(p => p.SteamId).HasColumnName("SteamId").IsRequired().HasMaxLength(17);
                b.Property(p => p.Team).HasColumnName("Team");
                b.Property(p => p.RatingStart).HasColumnName("RatingStart");
                b.Property(p => p.RatingEnd).HasColumnName("RatingEnd");
                b.Property(p => p.IsLeaver).HasColumnName("IsLeaver");
                b.HasKey("MatchId", "PlayerId");
                b.WithOwner().HasForeignKey("MatchId");
                b.HasOne<Player>().WithMany().HasForeignKey(p => p.PlayerId);

                b.OwnsOne(p => p.Stats, x =>
                    {
                        x.Property(s => s.Kills).HasColumnName("Stats_Kills");
                        x.Property(s => s.Assists).HasColumnName("Stats_Assists");
                        x.Property(s => s.Deaths).HasColumnName("Stats_Deaths");
                        x.Property(s => s.Hs).HasColumnName("Stats_Hs");
                    })
                    .Navigation(p => p.Stats).IsRequired();
            });
            
            builder.OwnsMany(m => m.Lobbies, b =>
            {
                b.ToTable("MatchesLobbies");
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