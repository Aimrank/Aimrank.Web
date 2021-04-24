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
            builder.ToTable("matches");
            
            builder.HasKey(m => m.Id);

            builder.Property<MatchWinner>("_winner").HasColumnName("winner");
            builder.Property<int>("_scoreT").HasColumnName("score_t");
            builder.Property<int>("_scoreCT").HasColumnName("score_ct");
            builder.Property<string>("_address").HasColumnName("address").HasMaxLength(50);
            builder.Property<DateTime>("_createdAt").HasColumnName("created_at");
            builder.Property(m => m.Mode).HasColumnName("mode");
            builder.Property(m => m.Map).IsRequired().HasMaxLength(50);
            
            builder.OwnsMany(m => m.Players, b =>
            {
                b.ToTable("matches_players");
                b.Property<MatchId>("MatchId");
                b.Property(p => p.PlayerId).HasColumnName("player_id").IsRequired();
                b.Property(p => p.SteamId).HasColumnName("steam_id").IsRequired().HasMaxLength(17);
                b.Property(p => p.Team).HasColumnName("team");
                b.Property(p => p.RatingStart).HasColumnName("rating_start");
                b.Property(p => p.RatingEnd).HasColumnName("rating_end");
                b.Property(p => p.IsLeaver).HasColumnName("is_leaver");
                b.HasKey("MatchId", "PlayerId");
                b.WithOwner().HasForeignKey("MatchId");
                b.HasOne<Player>().WithMany().HasForeignKey(p => p.PlayerId);

                b.OwnsOne(p => p.Stats, x =>
                    {
                        x.Property(s => s.Kills).HasColumnName("stats_kills");
                        x.Property(s => s.Assists).HasColumnName("stats_assists");
                        x.Property(s => s.Deaths).HasColumnName("stats_deaths");
                        x.Property(s => s.Hs).HasColumnName("stats_hs");
                    })
                    .Navigation(p => p.Stats).IsRequired();
            });
            
            builder.OwnsMany(m => m.Lobbies, b =>
            {
                b.ToTable("matches_lobbies");
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