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
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Lobbies", "aimrank");
                });

            modelBuilder.Entity("Aimrank.Domain.Matches.Match", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FinishedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Map")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ScoreCT")
                        .HasColumnType("int");

                    b.Property<int>("ScoreT")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Matches", "aimrank");
                });

            modelBuilder.Entity("Aimrank.Domain.RefreshTokens.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsInvalidated")
                        .HasColumnType("bit");

                    b.Property<string>("Jwt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens", "aimrank");
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
                        .HasColumnType("nvarchar(17)");

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
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.HasKey("LobbyId");

                            b1.ToTable("Lobbies");

                            b1.WithOwner()
                                .HasForeignKey("LobbyId");
                        });

                    b.OwnsMany("Aimrank.Domain.Lobbies.LobbyMember", "Members", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("LobbyId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Role")
                                .HasColumnType("int");

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

                    b.Navigation("Members");
                });

            modelBuilder.Entity("Aimrank.Domain.Matches.Match", b =>
                {
                    b.OwnsMany("Aimrank.Domain.Matches.MatchPlayer", "Players", b1 =>
                        {
                            b1.Property<Guid>("MatchId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Assists")
                                .HasColumnType("int");

                            b1.Property<int>("Deaths")
                                .HasColumnType("int");

                            b1.Property<int>("Kills")
                                .HasColumnType("int");

                            b1.Property<int>("Score")
                                .HasColumnType("int");

                            b1.Property<string>("SteamId")
                                .IsRequired()
                                .HasMaxLength(17)
                                .HasColumnType("nvarchar(17)");

                            b1.HasKey("MatchId", "UserId");

                            b1.HasIndex("UserId")
                                .IsUnique();

                            b1.ToTable("MatchesPlayers", "aimrank");

                            b1.WithOwner()
                                .HasForeignKey("MatchId");

                            b1.HasOne("Aimrank.Infrastructure.Domain.Users.UserModel", null)
                                .WithOne()
                                .HasForeignKey("Aimrank.Domain.Matches.MatchPlayer", "UserId")
                                .OnDelete(DeleteBehavior.Restrict)
                                .IsRequired();
                        });

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
