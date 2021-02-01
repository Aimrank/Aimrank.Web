using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aimrank.Database.Migrator.Migrations
{
    public partial class AddMatches : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MatchesPlayers",
                schema: "aimrank",
                table: "MatchesPlayers");

            migrationBuilder.DropIndex(
                name: "IX_MatchesPlayers_MatchId",
                schema: "aimrank",
                table: "MatchesPlayers");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "aimrank",
                table: "MatchesPlayers");

            migrationBuilder.DropColumn(
                name: "Team",
                schema: "aimrank",
                table: "MatchesPlayers");

            migrationBuilder.AlterColumn<string>(
                name: "SteamId",
                schema: "aimrank",
                table: "MatchesPlayers",
                type: "nvarchar(17)",
                maxLength: 17,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                schema: "aimrank",
                table: "MatchesPlayers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishedAt",
                schema: "aimrank",
                table: "Matches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Map",
                schema: "aimrank",
                table: "Matches",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "aimrank",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MatchesPlayers",
                schema: "aimrank",
                table: "MatchesPlayers",
                columns: new[] { "MatchId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_MatchesPlayers_UserId",
                schema: "aimrank",
                table: "MatchesPlayers",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MatchesPlayers_AspNetUsers_UserId",
                schema: "aimrank",
                table: "MatchesPlayers",
                column: "UserId",
                principalSchema: "aimrank",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchesPlayers_AspNetUsers_UserId",
                schema: "aimrank",
                table: "MatchesPlayers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MatchesPlayers",
                schema: "aimrank",
                table: "MatchesPlayers");

            migrationBuilder.DropIndex(
                name: "IX_MatchesPlayers_UserId",
                schema: "aimrank",
                table: "MatchesPlayers");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "aimrank",
                table: "MatchesPlayers");

            migrationBuilder.DropColumn(
                name: "FinishedAt",
                schema: "aimrank",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Map",
                schema: "aimrank",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "aimrank",
                table: "Matches");

            migrationBuilder.AlterColumn<string>(
                name: "SteamId",
                schema: "aimrank",
                table: "MatchesPlayers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(17)",
                oldMaxLength: 17);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "aimrank",
                table: "MatchesPlayers",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Team",
                schema: "aimrank",
                table: "MatchesPlayers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MatchesPlayers",
                schema: "aimrank",
                table: "MatchesPlayers",
                columns: new[] { "SteamId", "MatchId" });

            migrationBuilder.CreateIndex(
                name: "IX_MatchesPlayers_MatchId",
                schema: "aimrank",
                table: "MatchesPlayers",
                column: "MatchId");
        }
    }
}
