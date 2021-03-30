﻿// <auto-generated />
using System;
using Aimrank.Modules.UserAccess.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Aimrank.Database.Migrator.Migrations.UserAccess
{
    [DbContext(typeof(UserAccessContext))]
    [Migration("20210329212546_CreateUserTokens")]
    partial class CreateUserTokens
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Aimrank.Modules.UserAccess.Domain.Friendships.Friendship", b =>
                {
                    b.Property<Guid>("User1Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("User1Id");

                    b.Property<Guid>("User2Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("User2Id");

                    b.Property<Guid?>("_blockingUserId1")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("BlockingUserId1");

                    b.Property<Guid?>("_blockingUserId2")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("BlockingUserId2");

                    b.Property<DateTime>("_createdAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedAt");

                    b.Property<Guid?>("_invitingUserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("InvitingUserId");

                    b.Property<bool>("_isAccepted")
                        .HasColumnType("bit")
                        .HasColumnName("IsAccepted");

                    b.HasKey("User1Id", "User2Id");

                    b.HasIndex("User2Id");

                    b.HasIndex("_blockingUserId1");

                    b.HasIndex("_blockingUserId2");

                    b.HasIndex("_invitingUserId");

                    b.ToTable("Friendships", "users");
                });

            modelBuilder.Entity("Aimrank.Modules.UserAccess.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Email");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("IsActive");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Username");

                    b.Property<string>("_password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Password");

                    b.HasKey("Id");

                    b.ToTable("Users", "users");
                });

            modelBuilder.Entity("Aimrank.Modules.UserAccess.Domain.Friendships.Friendship", b =>
                {
                    b.HasOne("Aimrank.Modules.UserAccess.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("User1Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Aimrank.Modules.UserAccess.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("User2Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Aimrank.Modules.UserAccess.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("_blockingUserId1")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Aimrank.Modules.UserAccess.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("_blockingUserId2")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Aimrank.Modules.UserAccess.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("_invitingUserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Aimrank.Modules.UserAccess.Domain.Users.User", b =>
                {
                    b.OwnsMany("Aimrank.Modules.UserAccess.Domain.Users.UserToken", "_tokens", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Type")
                                .HasColumnType("int")
                                .HasColumnName("Type");

                            b1.Property<DateTime?>("ExpiresAt")
                                .HasColumnType("datetime2")
                                .HasColumnName("ExpiresAt");

                            b1.Property<string>("Token")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Token");

                            b1.HasKey("UserId", "Type");

                            b1.ToTable("UsersTokens", "users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("_tokens");
                });
#pragma warning restore 612, 618
        }
    }
}
