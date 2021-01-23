﻿// <auto-generated />
using System;
using Aimrank.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Aimrank.Web.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Aimrank.Web.Data.Match", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ScoreCT")
                        .HasColumnType("int");

                    b.Property<int>("ScoreT")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("Aimrank.Web.Data.Match", b =>
                {
                    b.OwnsMany("Aimrank.Web.Data.MatchPlayer", "Scoreboard", b1 =>
                        {
                            b1.Property<string>("SteamId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<Guid>("MatchId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Assists")
                                .HasColumnType("int");

                            b1.Property<int>("Deaths")
                                .HasColumnType("int");

                            b1.Property<int>("Kills")
                                .HasColumnType("int");

                            b1.Property<string>("Name")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("Score")
                                .HasColumnType("int");

                            b1.Property<int>("Team")
                                .HasColumnType("int");

                            b1.HasKey("SteamId", "MatchId");

                            b1.HasIndex("MatchId");

                            b1.ToTable("MatchPlayer");

                            b1.WithOwner()
                                .HasForeignKey("MatchId");
                        });

                    b.Navigation("Scoreboard");
                });
#pragma warning restore 612, 618
        }
    }
}
