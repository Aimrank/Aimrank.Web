using Microsoft.EntityFrameworkCore.Migrations;

namespace Aimrank.Database.Migrator.Migrations
{
    public partial class LobbiesMultipleMaps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Configuration_Map",
                schema: "matches",
                table: "Lobbies");

            migrationBuilder.AddColumn<string>(
                name: "Configuration_Maps",
                schema: "matches",
                table: "Lobbies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Configuration_Maps",
                schema: "matches",
                table: "Lobbies");

            migrationBuilder.AddColumn<string>(
                name: "Configuration_Map",
                schema: "matches",
                table: "Lobbies",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
