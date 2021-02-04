using Microsoft.EntityFrameworkCore.Migrations;

namespace Aimrank.Database.Migrator.Migrations
{
    public partial class AddAddressToMatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MatchesPlayers_UserId",
                schema: "aimrank",
                table: "MatchesPlayers");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                schema: "aimrank",
                table: "Matches",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MatchesPlayers_UserId",
                schema: "aimrank",
                table: "MatchesPlayers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MatchesPlayers_UserId",
                schema: "aimrank",
                table: "MatchesPlayers");

            migrationBuilder.DropColumn(
                name: "Address",
                schema: "aimrank",
                table: "Matches");

            migrationBuilder.CreateIndex(
                name: "IX_MatchesPlayers_UserId",
                schema: "aimrank",
                table: "MatchesPlayers",
                column: "UserId",
                unique: true);
        }
    }
}
