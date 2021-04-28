﻿// <auto-generated />
using System;
using Aimrank.Web.Modules.UserAccess.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Aimrank.Web.Database.Migrator.Migrations
{
    [DbContext(typeof(UserAccessContext))]
    partial class UserAccessContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("users")
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("Aimrank.Web.Modules.UserAccess.Domain.Friendships.Friendship", b =>
                {
                    b.Property<Guid>("User1Id")
                        .HasColumnType("uuid")
                        .HasColumnName("user_1_id");

                    b.Property<Guid>("User2Id")
                        .HasColumnType("uuid")
                        .HasColumnName("user_2_id");

                    b.Property<Guid?>("_blockingUserId1")
                        .HasColumnType("uuid")
                        .HasColumnName("blocking_user_id_1");

                    b.Property<Guid?>("_blockingUserId2")
                        .HasColumnType("uuid")
                        .HasColumnName("blocking_user_id_2");

                    b.Property<DateTime>("_createdAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid?>("_invitingUserId")
                        .HasColumnType("uuid")
                        .HasColumnName("inviting_user_id");

                    b.Property<bool>("_isAccepted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_accepted");

                    b.HasKey("User1Id", "User2Id")
                        .HasName("pk_friendships");

                    b.HasIndex("User2Id")
                        .HasDatabaseName("ix_friendships_user_2_id");

                    b.HasIndex("_blockingUserId1")
                        .HasDatabaseName("ix_friendships_blocking_user_id_1");

                    b.HasIndex("_blockingUserId2")
                        .HasDatabaseName("ix_friendships_blocking_user_id_2");

                    b.HasIndex("_invitingUserId")
                        .HasDatabaseName("ix_friendships_inviting_user_id");

                    b.ToTable("friendships");
                });

            modelBuilder.Entity("Aimrank.Web.Modules.UserAccess.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(320)
                        .HasColumnType("character varying(320)")
                        .HasColumnName("email");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("username");

                    b.Property<string>("_password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.Processing.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("data");

                    b.Property<DateTime>("OccurredOn")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("occurred_on");

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

            modelBuilder.Entity("Aimrank.Web.Modules.UserAccess.Domain.Friendships.Friendship", b =>
                {
                    b.HasOne("Aimrank.Web.Modules.UserAccess.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("User1Id")
                        .HasConstraintName("fk_friendships_users_user_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Aimrank.Web.Modules.UserAccess.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("User2Id")
                        .HasConstraintName("fk_friendships_users_user_id1")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Aimrank.Web.Modules.UserAccess.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("_blockingUserId1")
                        .HasConstraintName("fk_friendships_users_user_id2")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Aimrank.Web.Modules.UserAccess.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("_blockingUserId2")
                        .HasConstraintName("fk_friendships_users_user_id3")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Aimrank.Web.Modules.UserAccess.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("_invitingUserId")
                        .HasConstraintName("fk_friendships_users_user_id4")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Aimrank.Web.Modules.UserAccess.Domain.Users.User", b =>
                {
                    b.OwnsMany("Aimrank.Web.Modules.UserAccess.Domain.Users.UserRole", "_roles", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid")
                                .HasColumnName("user_id");

                            b1.Property<string>("Name")
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("name");

                            b1.HasKey("UserId", "Name")
                                .HasName("pk_users_roles");

                            b1.ToTable("users_roles");

                            b1.WithOwner()
                                .HasForeignKey("UserId")
                                .HasConstraintName("fk_users_roles_users_user_id");
                        });

                    b.OwnsMany("Aimrank.Web.Modules.UserAccess.Domain.Users.UserToken", "_tokens", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid")
                                .HasColumnName("user_id");

                            b1.Property<int>("Type")
                                .HasColumnType("integer")
                                .HasColumnName("type");

                            b1.Property<DateTime?>("ExpiresAt")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("expires_at");

                            b1.Property<string>("Token")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("token");

                            b1.HasKey("UserId", "Type")
                                .HasName("pk_users_tokens");

                            b1.ToTable("users_tokens");

                            b1.WithOwner()
                                .HasForeignKey("UserId")
                                .HasConstraintName("fk_users_tokens_users_user_id");
                        });

                    b.Navigation("_roles");

                    b.Navigation("_tokens");
                });
#pragma warning restore 612, 618
        }
    }
}
