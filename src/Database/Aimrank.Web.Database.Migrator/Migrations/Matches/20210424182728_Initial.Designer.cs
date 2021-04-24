﻿// <auto-generated />
using System;
using Aimrank.Web.Modules.Matches.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Aimrank.Web.Database.Migrator.Migrations.Matches
{
    [DbContext(typeof(MatchesContext))]
    [Migration("20210424182728_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("matches")
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("Aimrank.Web.Modules.Matches.Domain.Lobbies.Lobby", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.HasKey("Id")
                        .HasName("pk_lobbies");

                    b.ToTable("lobbies");
                });

            modelBuilder.Entity("Aimrank.Web.Modules.Matches.Domain.Matches.Match", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime?>("FinishedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("finished_at");

                    b.Property<string>("Map")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("map");

                    b.Property<int>("Mode")
                        .HasColumnType("integer")
                        .HasColumnName("mode");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<string>("_address")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("address");

                    b.Property<DateTime>("_createdAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<int>("_scoreCT")
                        .HasColumnType("integer")
                        .HasColumnName("score_ct");

                    b.Property<int>("_scoreT")
                        .HasColumnType("integer")
                        .HasColumnName("score_t");

                    b.Property<int>("_winner")
                        .HasColumnType("integer")
                        .HasColumnName("winner");

                    b.HasKey("Id")
                        .HasName("pk_matches");

                    b.ToTable("matches");
                });

            modelBuilder.Entity("Aimrank.Web.Modules.Matches.Domain.Players.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("SteamId")
                        .IsRequired()
                        .HasMaxLength(17)
                        .HasColumnType("character varying(17)")
                        .HasColumnName("steam_id");

                    b.HasKey("Id")
                        .HasName("pk_players");

                    b.ToTable("players");
                });

            modelBuilder.Entity("Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Processing.Inbox.InboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("data");

                    b.Property<DateTime>("OccurredAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("occurred_at");

                    b.Property<DateTime?>("ProcessedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("processed_date");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_inbox_messages");

                    b.ToTable("inbox_messages");
                });

            modelBuilder.Entity("Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Processing.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("data");

                    b.Property<DateTime>("OccurredAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("occurred_at");

                    b.Property<DateTime?>("ProcessedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("processed_date");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_outbox_messages");

                    b.ToTable("outbox_messages");
                });

            modelBuilder.Entity("Aimrank.Web.Modules.Matches.Domain.Lobbies.Lobby", b =>
                {
                    b.OwnsOne("Aimrank.Web.Modules.Matches.Domain.Lobbies.LobbyConfiguration", "Configuration", b1 =>
                        {
                            b1.Property<Guid>("LobbyId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<int>("Mode")
                                .HasColumnType("integer")
                                .HasColumnName("configuration_mode");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(450)
                                .HasColumnType("character varying(450)")
                                .HasColumnName("configuration_name");

                            b1.Property<string>("_maps")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("configuration_maps");

                            b1.HasKey("LobbyId")
                                .HasName("pk_lobbies");

                            b1.ToTable("lobbies");

                            b1.WithOwner()
                                .HasForeignKey("LobbyId")
                                .HasConstraintName("fk_lobbies_lobbies_id");
                        });

                    b.OwnsMany("Aimrank.Web.Modules.Matches.Domain.Lobbies.LobbyInvitation", "Invitations", b1 =>
                        {
                            b1.Property<Guid>("LobbyId")
                                .HasColumnType("uuid")
                                .HasColumnName("lobby_id");

                            b1.Property<Guid>("InvitingPlayerId")
                                .HasColumnType("uuid")
                                .HasColumnName("inviting_player_id");

                            b1.Property<Guid>("InvitedPlayerId")
                                .HasColumnType("uuid")
                                .HasColumnName("invited_player_id");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("created_at");

                            b1.HasKey("LobbyId", "InvitingPlayerId", "InvitedPlayerId")
                                .HasName("pk_lobbies_invitations");

                            b1.HasIndex("InvitedPlayerId")
                                .HasDatabaseName("ix_lobbies_invitations_invited_player_id");

                            b1.HasIndex("InvitingPlayerId")
                                .HasDatabaseName("ix_lobbies_invitations_inviting_player_id");

                            b1.ToTable("lobbies_invitations");

                            b1.HasOne("Aimrank.Web.Modules.Matches.Domain.Players.Player", null)
                                .WithMany()
                                .HasForeignKey("InvitedPlayerId")
                                .HasConstraintName("fk_lobbies_invitations_players_player_id")
                                .OnDelete(DeleteBehavior.Restrict)
                                .IsRequired();

                            b1.HasOne("Aimrank.Web.Modules.Matches.Domain.Players.Player", null)
                                .WithMany()
                                .HasForeignKey("InvitingPlayerId")
                                .HasConstraintName("fk_lobbies_invitations_players_player_id1")
                                .OnDelete(DeleteBehavior.Restrict)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("LobbyId")
                                .HasConstraintName("fk_lobbies_invitations_lobbies_lobby_id");
                        });

                    b.OwnsMany("Aimrank.Web.Modules.Matches.Domain.Lobbies.LobbyMember", "Members", b1 =>
                        {
                            b1.Property<Guid>("PlayerId")
                                .HasColumnType("uuid")
                                .HasColumnName("player_id");

                            b1.Property<Guid>("LobbyId")
                                .HasColumnType("uuid")
                                .HasColumnName("lobby_id");

                            b1.Property<int>("Role")
                                .HasColumnType("integer")
                                .HasColumnName("role");

                            b1.HasKey("PlayerId")
                                .HasName("pk_lobbies_members");

                            b1.HasIndex("LobbyId")
                                .HasDatabaseName("ix_lobbies_members_lobby_id");

                            b1.ToTable("lobbies_members");

                            b1.WithOwner()
                                .HasForeignKey("LobbyId")
                                .HasConstraintName("fk_lobbies_members_lobbies_lobby_id");

                            b1.HasOne("Aimrank.Web.Modules.Matches.Domain.Players.Player", null)
                                .WithOne()
                                .HasForeignKey("Aimrank.Web.Modules.Matches.Domain.Lobbies.LobbyMember", "PlayerId")
                                .HasConstraintName("fk_lobbies_members_players_player_id1")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();
                        });

                    b.Navigation("Configuration")
                        .IsRequired();

                    b.Navigation("Invitations");

                    b.Navigation("Members");
                });

            modelBuilder.Entity("Aimrank.Web.Modules.Matches.Domain.Matches.Match", b =>
                {
                    b.OwnsMany("Aimrank.Web.Modules.Matches.Domain.Matches.MatchLobby", "Lobbies", b1 =>
                        {
                            b1.Property<Guid>("MatchId")
                                .HasColumnType("uuid")
                                .HasColumnName("match_id");

                            b1.Property<Guid>("LobbyId")
                                .HasColumnType("uuid")
                                .HasColumnName("lobby_id");

                            b1.HasKey("MatchId", "LobbyId")
                                .HasName("pk_matches_lobbies");

                            b1.HasIndex("LobbyId")
                                .IsUnique()
                                .HasDatabaseName("ix_matches_lobbies_lobby_id");

                            b1.ToTable("matches_lobbies");

                            b1.HasOne("Aimrank.Web.Modules.Matches.Domain.Lobbies.Lobby", null)
                                .WithOne()
                                .HasForeignKey("Aimrank.Web.Modules.Matches.Domain.Matches.MatchLobby", "LobbyId")
                                .HasConstraintName("fk_matches_lobbies_lobbies_lobby_id")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("MatchId")
                                .HasConstraintName("fk_matches_lobbies_matches_match_id");
                        });

                    b.OwnsMany("Aimrank.Web.Modules.Matches.Domain.Matches.MatchPlayer", "Players", b1 =>
                        {
                            b1.Property<Guid>("MatchId")
                                .HasColumnType("uuid")
                                .HasColumnName("match_id");

                            b1.Property<Guid>("PlayerId")
                                .HasColumnType("uuid")
                                .HasColumnName("player_id");

                            b1.Property<bool>("IsLeaver")
                                .HasColumnType("boolean")
                                .HasColumnName("is_leaver");

                            b1.Property<int>("RatingEnd")
                                .HasColumnType("integer")
                                .HasColumnName("rating_end");

                            b1.Property<int>("RatingStart")
                                .HasColumnType("integer")
                                .HasColumnName("rating_start");

                            b1.Property<string>("SteamId")
                                .IsRequired()
                                .HasMaxLength(17)
                                .HasColumnType("character varying(17)")
                                .HasColumnName("steam_id");

                            b1.Property<int>("Team")
                                .HasColumnType("integer")
                                .HasColumnName("team");

                            b1.HasKey("MatchId", "PlayerId")
                                .HasName("pk_matches_players");

                            b1.HasIndex("PlayerId")
                                .HasDatabaseName("ix_matches_players_player_id");

                            b1.ToTable("matches_players");

                            b1.WithOwner()
                                .HasForeignKey("MatchId")
                                .HasConstraintName("fk_matches_players_matches_match_id");

                            b1.HasOne("Aimrank.Web.Modules.Matches.Domain.Players.Player", null)
                                .WithMany()
                                .HasForeignKey("PlayerId")
                                .HasConstraintName("fk_matches_players_players_player_id")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.OwnsOne("Aimrank.Web.Modules.Matches.Domain.Matches.MatchPlayerStats", "Stats", b2 =>
                                {
                                    b2.Property<Guid>("MatchPlayerMatchId")
                                        .HasColumnType("uuid")
                                        .HasColumnName("match_id");

                                    b2.Property<Guid>("MatchPlayerPlayerId")
                                        .HasColumnType("uuid")
                                        .HasColumnName("player_id");

                                    b2.Property<int>("Assists")
                                        .HasColumnType("integer")
                                        .HasColumnName("stats_assists");

                                    b2.Property<int>("Deaths")
                                        .HasColumnType("integer")
                                        .HasColumnName("stats_deaths");

                                    b2.Property<int>("Hs")
                                        .HasColumnType("integer")
                                        .HasColumnName("stats_hs");

                                    b2.Property<int>("Kills")
                                        .HasColumnType("integer")
                                        .HasColumnName("stats_kills");

                                    b2.HasKey("MatchPlayerMatchId", "MatchPlayerPlayerId")
                                        .HasName("pk_matches_players");

                                    b2.ToTable("matches_players");

                                    b2.WithOwner()
                                        .HasForeignKey("MatchPlayerMatchId", "MatchPlayerPlayerId")
                                        .HasConstraintName("fk_matches_players_matches_players_match_id_player_id");
                                });

                            b1.Navigation("Stats")
                                .IsRequired();
                        });

                    b.Navigation("Lobbies");

                    b.Navigation("Players");
                });
#pragma warning restore 612, 618
        }
    }
}
