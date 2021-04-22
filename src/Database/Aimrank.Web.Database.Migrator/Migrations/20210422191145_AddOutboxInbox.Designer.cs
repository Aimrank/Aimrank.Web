﻿// <auto-generated />
using System;
using Aimrank.Web.Modules.Matches.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Aimrank.Web.Database.Migrator.Migrations
{
    [DbContext(typeof(MatchesContext))]
    [Migration("20210422191145_AddOutboxInbox")]
    partial class AddOutboxInbox
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Aimrank.Web.Modules.Matches.Domain.Lobbies.Lobby", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("Status");

                    b.HasKey("Id");

                    b.ToTable("Lobbies", "matches");
                });

            modelBuilder.Entity("Aimrank.Web.Modules.Matches.Domain.Matches.Match", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("FinishedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("FinishedAt");

                    b.Property<string>("Map")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Map");

                    b.Property<int>("Mode")
                        .HasColumnType("int")
                        .HasColumnName("Mode");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("Status");

                    b.Property<string>("_address")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Address");

                    b.Property<DateTime>("_createdAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedAt");

                    b.Property<int>("_scoreCT")
                        .HasColumnType("int")
                        .HasColumnName("ScoreCT");

                    b.Property<int>("_scoreT")
                        .HasColumnType("int")
                        .HasColumnName("ScoreT");

                    b.Property<int>("_winner")
                        .HasColumnType("int")
                        .HasColumnName("Winner");

                    b.HasKey("Id");

                    b.ToTable("Matches", "matches");
                });

            modelBuilder.Entity("Aimrank.Web.Modules.Matches.Domain.Players.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SteamId")
                        .IsRequired()
                        .HasMaxLength(17)
                        .HasColumnType("nvarchar(17)")
                        .HasColumnName("SteamId");

                    b.HasKey("Id");

                    b.ToTable("Players", "matches");
                });

            modelBuilder.Entity("Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Processing.Inbox.InboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OccurredAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ProcessedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("InboxMessages", "matches");
                });

            modelBuilder.Entity("Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Processing.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OccurredAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ProcessedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("OutboxMessages", "matches");
                });

            modelBuilder.Entity("Aimrank.Web.Modules.Matches.Domain.Lobbies.Lobby", b =>
                {
                    b.OwnsOne("Aimrank.Web.Modules.Matches.Domain.Lobbies.LobbyConfiguration", "Configuration", b1 =>
                        {
                            b1.Property<Guid>("LobbyId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Mode")
                                .HasColumnType("int")
                                .HasColumnName("Configuration_Mode");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(450)
                                .HasColumnType("nvarchar(450)")
                                .HasColumnName("Configuration_Name");

                            b1.Property<string>("_maps")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Configuration_Maps");

                            b1.HasKey("LobbyId");

                            b1.ToTable("Lobbies");

                            b1.WithOwner()
                                .HasForeignKey("LobbyId");
                        });

                    b.OwnsMany("Aimrank.Web.Modules.Matches.Domain.Lobbies.LobbyInvitation", "Invitations", b1 =>
                        {
                            b1.Property<Guid>("LobbyId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("InvitingPlayerId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("InvitingPlayerId");

                            b1.Property<Guid>("InvitedPlayerId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("InvitedPlayerId");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("datetime2")
                                .HasColumnName("CreatedAt");

                            b1.HasKey("LobbyId", "InvitingPlayerId", "InvitedPlayerId");

                            b1.HasIndex("InvitedPlayerId");

                            b1.HasIndex("InvitingPlayerId");

                            b1.ToTable("LobbiesInvitations", "matches");

                            b1.HasOne("Aimrank.Web.Modules.Matches.Domain.Players.Player", null)
                                .WithMany()
                                .HasForeignKey("InvitedPlayerId")
                                .OnDelete(DeleteBehavior.Restrict)
                                .IsRequired();

                            b1.HasOne("Aimrank.Web.Modules.Matches.Domain.Players.Player", null)
                                .WithMany()
                                .HasForeignKey("InvitingPlayerId")
                                .OnDelete(DeleteBehavior.Restrict)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("LobbyId");
                        });

                    b.OwnsMany("Aimrank.Web.Modules.Matches.Domain.Lobbies.LobbyMember", "Members", b1 =>
                        {
                            b1.Property<Guid>("PlayerId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("PlayerId");

                            b1.Property<Guid>("LobbyId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Role")
                                .HasColumnType("int")
                                .HasColumnName("Role");

                            b1.HasKey("PlayerId");

                            b1.HasIndex("LobbyId");

                            b1.ToTable("LobbiesMembers", "matches");

                            b1.WithOwner()
                                .HasForeignKey("LobbyId");

                            b1.HasOne("Aimrank.Web.Modules.Matches.Domain.Players.Player", null)
                                .WithOne()
                                .HasForeignKey("Aimrank.Web.Modules.Matches.Domain.Lobbies.LobbyMember", "PlayerId")
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
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("LobbyId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("MatchId", "LobbyId");

                            b1.HasIndex("LobbyId")
                                .IsUnique();

                            b1.ToTable("MatchesLobbies", "matches");

                            b1.HasOne("Aimrank.Web.Modules.Matches.Domain.Lobbies.Lobby", null)
                                .WithOne()
                                .HasForeignKey("Aimrank.Web.Modules.Matches.Domain.Matches.MatchLobby", "LobbyId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("MatchId");
                        });

                    b.OwnsMany("Aimrank.Web.Modules.Matches.Domain.Matches.MatchPlayer", "Players", b1 =>
                        {
                            b1.Property<Guid>("MatchId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("PlayerId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("PlayerId");

                            b1.Property<bool>("IsLeaver")
                                .HasColumnType("bit")
                                .HasColumnName("IsLeaver");

                            b1.Property<int>("RatingEnd")
                                .HasColumnType("int")
                                .HasColumnName("RatingEnd");

                            b1.Property<int>("RatingStart")
                                .HasColumnType("int")
                                .HasColumnName("RatingStart");

                            b1.Property<string>("SteamId")
                                .IsRequired()
                                .HasMaxLength(17)
                                .HasColumnType("nvarchar(17)")
                                .HasColumnName("SteamId");

                            b1.Property<int>("Team")
                                .HasColumnType("int")
                                .HasColumnName("Team");

                            b1.HasKey("MatchId", "PlayerId");

                            b1.HasIndex("PlayerId");

                            b1.ToTable("MatchesPlayers", "matches");

                            b1.WithOwner()
                                .HasForeignKey("MatchId");

                            b1.HasOne("Aimrank.Web.Modules.Matches.Domain.Players.Player", null)
                                .WithMany()
                                .HasForeignKey("PlayerId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.OwnsOne("Aimrank.Web.Modules.Matches.Domain.Matches.MatchPlayerStats", "Stats", b2 =>
                                {
                                    b2.Property<Guid>("MatchPlayerMatchId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<Guid>("MatchPlayerPlayerId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<int>("Assists")
                                        .HasColumnType("int")
                                        .HasColumnName("Stats_Assists");

                                    b2.Property<int>("Deaths")
                                        .HasColumnType("int")
                                        .HasColumnName("Stats_Deaths");

                                    b2.Property<int>("Hs")
                                        .HasColumnType("int")
                                        .HasColumnName("Stats_Hs");

                                    b2.Property<int>("Kills")
                                        .HasColumnType("int")
                                        .HasColumnName("Stats_Kills");

                                    b2.HasKey("MatchPlayerMatchId", "MatchPlayerPlayerId");

                                    b2.ToTable("MatchesPlayers");

                                    b2.WithOwner()
                                        .HasForeignKey("MatchPlayerMatchId", "MatchPlayerPlayerId");
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
