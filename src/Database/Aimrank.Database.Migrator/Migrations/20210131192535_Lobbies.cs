using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aimrank.Database.Migrator.Migrations
{
    public partial class Lobbies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lobbies",
                schema: "aimrank",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Configuration_Map = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lobbies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LobbiesMembers",
                schema: "aimrank",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    LobbyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LobbiesMembers", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_LobbiesMembers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "aimrank",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LobbiesMembers_Lobbies_LobbyId",
                        column: x => x.LobbyId,
                        principalSchema: "aimrank",
                        principalTable: "Lobbies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LobbiesMembers_LobbyId",
                schema: "aimrank",
                table: "LobbiesMembers",
                column: "LobbyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LobbiesMembers",
                schema: "aimrank");

            migrationBuilder.DropTable(
                name: "Lobbies",
                schema: "aimrank");
        }
    }
}
