using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aimrank.Web.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matches",
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
                name: "MatchPlayer",
                columns: table => new
                {
                    SteamId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Team = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Kills = table.Column<int>(type: "int", nullable: false),
                    Deaths = table.Column<int>(type: "int", nullable: false),
                    Assists = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchPlayer", x => new { x.SteamId, x.MatchId });
                    table.ForeignKey(
                        name: "FK_MatchPlayer_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatchPlayer_MatchId",
                table: "MatchPlayer",
                column: "MatchId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchPlayer");

            migrationBuilder.DropTable(
                name: "Matches");
        }
    }
}
