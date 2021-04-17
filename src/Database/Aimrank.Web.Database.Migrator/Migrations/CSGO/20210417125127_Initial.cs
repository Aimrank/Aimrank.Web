using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aimrank.Web.Database.Migrator.Migrations.CSGO
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "csgo");

            migrationBuilder.CreateTable(
                name: "Pods",
                schema: "csgo",
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
                schema: "csgo",
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
                schema: "csgo",
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
                        principalSchema: "csgo",
                        principalTable: "Pods",
                        principalColumn: "IpAddress",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Servers_SteamTokens_SteamTokenToken",
                        column: x => x.SteamTokenToken,
                        principalSchema: "csgo",
                        principalTable: "SteamTokens",
                        principalColumn: "Token",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Servers_PodIpAddress",
                schema: "csgo",
                table: "Servers",
                column: "PodIpAddress");

            migrationBuilder.CreateIndex(
                name: "IX_Servers_SteamTokenToken",
                schema: "csgo",
                table: "Servers",
                column: "SteamTokenToken",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Servers",
                schema: "csgo");

            migrationBuilder.DropTable(
                name: "Pods",
                schema: "csgo");

            migrationBuilder.DropTable(
                name: "SteamTokens",
                schema: "csgo");
        }
    }
}
