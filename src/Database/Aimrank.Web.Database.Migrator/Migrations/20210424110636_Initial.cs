using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aimrank.Web.Database.Migrator.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cluster");

            migrationBuilder.CreateTable(
                name: "InboxMessages",
                schema: "cluster",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OccurredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InboxMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pods",
                schema: "cluster",
                columns: table => new
                {
                    IpAddress = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaxServers = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pods", x => x.IpAddress);
                });

            migrationBuilder.CreateTable(
                name: "SteamTokens",
                schema: "cluster",
                columns: table => new
                {
                    Token = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SteamTokens", x => x.Token);
                });

            migrationBuilder.CreateTable(
                name: "Servers",
                schema: "cluster",
                columns: table => new
                {
                    MatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PodIpAddress = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SteamTokenToken = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.MatchId);
                    table.ForeignKey(
                        name: "FK_Servers_Pods_PodIpAddress",
                        column: x => x.PodIpAddress,
                        principalSchema: "cluster",
                        principalTable: "Pods",
                        principalColumn: "IpAddress",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Servers_SteamTokens_SteamTokenToken",
                        column: x => x.SteamTokenToken,
                        principalSchema: "cluster",
                        principalTable: "SteamTokens",
                        principalColumn: "Token",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Servers_PodIpAddress",
                schema: "cluster",
                table: "Servers",
                column: "PodIpAddress");

            migrationBuilder.CreateIndex(
                name: "IX_Servers_SteamTokenToken",
                schema: "cluster",
                table: "Servers",
                column: "SteamTokenToken",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InboxMessages",
                schema: "cluster");

            migrationBuilder.DropTable(
                name: "Servers",
                schema: "cluster");

            migrationBuilder.DropTable(
                name: "Pods",
                schema: "cluster");

            migrationBuilder.DropTable(
                name: "SteamTokens",
                schema: "cluster");
        }
    }
}
