using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aimrank.Database.Migrator.Migrations
{
    public partial class AddMatchLobbyOneToOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MatchId",
                schema: "aimrank",
                table: "Lobbies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lobbies_MatchId",
                schema: "aimrank",
                table: "Lobbies",
                column: "MatchId",
                unique: true,
                filter: "[MatchId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Lobbies_Matches_MatchId",
                schema: "aimrank",
                table: "Lobbies",
                column: "MatchId",
                principalSchema: "aimrank",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lobbies_Matches_MatchId",
                schema: "aimrank",
                table: "Lobbies");

            migrationBuilder.DropIndex(
                name: "IX_Lobbies_MatchId",
                schema: "aimrank",
                table: "Lobbies");

            migrationBuilder.DropColumn(
                name: "MatchId",
                schema: "aimrank",
                table: "Lobbies");
        }
    }
}
