﻿// <auto-generated />
using System;
using Aimrank.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Aimrank.Database.Migrator.Migrations
{
    [DbContext(typeof(AimrankContext))]
    partial class AimrankContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("aimrank")
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Aimrank.Domain.Lobbies.Lobby", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("Status");

                    b.HasKey("Id");

                    b.ToTable("Lobbies", "aimrank");
                });

            modelBuilder.Entity("Aimrank.Domain.Matches.Match", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("Address")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Address");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedAt");

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

                    b.Property<int>("ScoreCT")
                        .HasColumnType("int")
                        .HasColumnName("ScoreCT");

                    b.Property<int>("ScoreT")
                        .HasColumnType("int")
                        .HasColumnName("ScoreT");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("Status");

                    b.HasKey("Id");

                    b.ToTable("Matches", "aimrank");
                });

            modelBuilder.Entity("Aimrank.Domain.RefreshTokens.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("ExpiresAt");

                    b.Property<bool>("IsInvalidated")
                        .HasColumnType("bit")
                        .HasColumnName("IsInvalidated");

                    b.Property<string>("Jwt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Jwt");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens", "aimrank");
                });

            modelBuilder.Entity("Aimrank.Infrastructure.Configuration.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OccurredAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("OutboxMessages", "aimrank");
                });

            modelBuilder.Entity("Aimrank.Infrastructure.Domain.Users.UserModel", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SteamId")
                        .HasMaxLength(17)
                        .HasColumnType("nvarchar(17)")
                        .HasColumnName("SteamId");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("SteamId")
                        .IsUnique()
                        .HasFilter("[SteamId] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<Aimrank.Domain.Users.UserId>", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<Aimrank.Domain.Users.UserId>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<Aimrank.Domain.Users.UserId>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<Aimrank.Domain.Users.UserId>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<Aimrank.Domain.Users.UserId>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<Aimrank.Domain.Users.UserId>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Aimrank.Domain.Lobbies.Lobby", b =>
                {
                    b.OwnsOne("Aimrank.Domain.Lobbies.LobbyConfiguration", "Configuration", b1 =>
                        {
                            b1.Property<Guid>("LobbyId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Map")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("Configuration_Map");

                            b1.Property<int>("Mode")
                                .HasColumnType("int")
                                .HasColumnName("Configuration_Mode");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(450)
                                .HasColumnType("nvarchar(450)")
                                .HasColumnName("Configuration_Name");

                            b1.HasKey("LobbyId");

                            b1.ToTable("Lobbies");

                            b1.WithOwner()
                                .HasForeignKey("LobbyId");
                        });

                    b.OwnsMany("Aimrank.Domain.Lobbies.LobbyInvitation", "Invitations", b1 =>
                        {
                            b1.Property<Guid>("LobbyId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("InvitingUserId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("InvitingUserId");

                            b1.Property<Guid>("InvitedUserId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("InvitedUserId");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("datetime2")
                                .HasColumnName("CreatedAt");

                            b1.HasKey("LobbyId", "InvitingUserId", "InvitedUserId");

                            b1.HasIndex("InvitedUserId");

                            b1.HasIndex("InvitingUserId");

                            b1.ToTable("LobbiesInvitations", "aimrank");

                            b1.HasOne("Aimrank.Infrastructure.Domain.Users.UserModel", null)
                                .WithMany()
                                .HasForeignKey("InvitedUserId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.HasOne("Aimrank.Infrastructure.Domain.Users.UserModel", null)
                                .WithMany()
                                .HasForeignKey("InvitingUserId")
                                .OnDelete(DeleteBehavior.Restrict)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("LobbyId");
                        });

                    b.OwnsMany("Aimrank.Domain.Lobbies.LobbyMember", "Members", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("UserId");

                            b1.Property<Guid>("LobbyId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Role")
                                .HasColumnType("int")
                                .HasColumnName("Role");

                            b1.HasKey("UserId");

                            b1.HasIndex("LobbyId");

                            b1.ToTable("LobbiesMembers", "aimrank");

                            b1.WithOwner()
                                .HasForeignKey("LobbyId");

                            b1.HasOne("Aimrank.Infrastructure.Domain.Users.UserModel", null)
                                .WithOne()
                                .HasForeignKey("Aimrank.Domain.Lobbies.LobbyMember", "UserId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();
                        });

                    b.Navigation("Configuration");

                    b.Navigation("Invitations");

                    b.Navigation("Members");
                });

            modelBuilder.Entity("Aimrank.Domain.Matches.Match", b =>
                {
                    b.OwnsMany("Aimrank.Domain.Matches.MatchLobby", "Lobbies", b1 =>
                        {
                            b1.Property<Guid>("MatchId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("LobbyId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("MatchId", "LobbyId");

                            b1.HasIndex("LobbyId")
                                .IsUnique();

                            b1.ToTable("MatchesLobbies", "aimrank");

                            b1.HasOne("Aimrank.Domain.Lobbies.Lobby", null)
                                .WithOne()
                                .HasForeignKey("Aimrank.Domain.Matches.MatchLobby", "LobbyId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("MatchId");
                        });

                    b.OwnsMany("Aimrank.Domain.Matches.MatchPlayer", "Players", b1 =>
                        {
                            b1.Property<Guid>("MatchId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("UserId");

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

                            b1.HasKey("MatchId", "UserId");

                            b1.HasIndex("UserId");

                            b1.ToTable("MatchesPlayers", "aimrank");

                            b1.WithOwner()
                                .HasForeignKey("MatchId");

                            b1.HasOne("Aimrank.Infrastructure.Domain.Users.UserModel", null)
                                .WithMany()
                                .HasForeignKey("UserId")
                                .OnDelete(DeleteBehavior.Restrict)
                                .IsRequired();

                            b1.OwnsOne("Aimrank.Domain.Matches.MatchPlayerStats", "Stats", b2 =>
                                {
                                    b2.Property<Guid>("MatchPlayerMatchId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<Guid>("MatchPlayerUserId")
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

                                    b2.Property<int>("Score")
                                        .HasColumnType("int")
                                        .HasColumnName("Stats_Score");

                                    b2.HasKey("MatchPlayerMatchId", "MatchPlayerUserId");

                                    b2.ToTable("MatchesPlayers");

                                    b2.WithOwner()
                                        .HasForeignKey("MatchPlayerMatchId", "MatchPlayerUserId");
                                });

                            b1.Navigation("Stats")
                                .IsRequired();
                        });

                    b.Navigation("Lobbies");

                    b.Navigation("Players");
                });

            modelBuilder.Entity("Aimrank.Domain.RefreshTokens.RefreshToken", b =>
                {
                    b.HasOne("Aimrank.Infrastructure.Domain.Users.UserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<Aimrank.Domain.Users.UserId>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<Aimrank.Domain.Users.UserId>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<Aimrank.Domain.Users.UserId>", b =>
                {
                    b.HasOne("Aimrank.Infrastructure.Domain.Users.UserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<Aimrank.Domain.Users.UserId>", b =>
                {
                    b.HasOne("Aimrank.Infrastructure.Domain.Users.UserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<Aimrank.Domain.Users.UserId>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<Aimrank.Domain.Users.UserId>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aimrank.Infrastructure.Domain.Users.UserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<Aimrank.Domain.Users.UserId>", b =>
                {
                    b.HasOne("Aimrank.Infrastructure.Domain.Users.UserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
