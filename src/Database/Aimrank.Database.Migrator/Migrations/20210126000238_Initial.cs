using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aimrank.Database.Migrator.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "aimrank");

            migrationBuilder.CreateTable(
                name: "Matches",
                schema: "aimrank",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScoreT = table.Column<int>(type: "int", nullable: false),
                    ScoreCT = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatchesPlayers",
                schema: "aimrank",
                columns: table => new
                {
                    SteamId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Team = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Kills = table.Column<int>(type: "int", nullable: false),
                    Deaths = table.Column<int>(type: "int", nullable: false),
                    Assists = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchesPlayers", x => new { x.SteamId, x.MatchId });
                    table.ForeignKey(
                        name: "FK_MatchesPlayers_Matches_MatchId",
                        column: x => x.MatchId,
                        principalSchema: "aimrank",
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatchesPlayers_MatchId",
                schema: "aimrank",
                table: "MatchesPlayers",
                column: "MatchId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchesPlayers",
                schema: "aimrank");

            migrationBuilder.DropTable(
                name: "Matches",
                schema: "aimrank");
        }
    }
}
