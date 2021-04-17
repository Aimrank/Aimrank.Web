using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aimrank.Web.Database.Migrator.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "matches");

            migrationBuilder.CreateTable(
                name: "Lobbies",
                schema: "matches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Configuration_Name = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Configuration_Map = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Configuration_Mode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lobbies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                schema: "matches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Map = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Mode = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    FinishedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScoreCT = table.Column<int>(type: "int", nullable: false),
                    ScoreT = table.Column<int>(type: "int", nullable: false),
                    Winner = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessages",
                schema: "matches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OccurredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                schema: "matches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SteamId = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatchesLobbies",
                schema: "matches",
                columns: table => new
                {
                    LobbyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchesLobbies", x => new { x.MatchId, x.LobbyId });
                    table.ForeignKey(
                        name: "FK_MatchesLobbies_Lobbies_LobbyId",
                        column: x => x.LobbyId,
                        principalSchema: "matches",
                        principalTable: "Lobbies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchesLobbies_Matches_MatchId",
                        column: x => x.MatchId,
                        principalSchema: "matches",
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LobbiesInvitations",
                schema: "matches",
                columns: table => new
                {
                    InvitingPlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvitedPlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LobbyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LobbiesInvitations", x => new { x.LobbyId, x.InvitingPlayerId, x.InvitedPlayerId });
                    table.ForeignKey(
                        name: "FK_LobbiesInvitations_Lobbies_LobbyId",
                        column: x => x.LobbyId,
                        principalSchema: "matches",
                        principalTable: "Lobbies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LobbiesInvitations_Players_InvitedPlayerId",
                        column: x => x.InvitedPlayerId,
                        principalSchema: "matches",
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LobbiesInvitations_Players_InvitingPlayerId",
                        column: x => x.InvitingPlayerId,
                        principalSchema: "matches",
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LobbiesMembers",
                schema: "matches",
                columns: table => new
                {
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    LobbyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LobbiesMembers", x => x.PlayerId);
                    table.ForeignKey(
                        name: "FK_LobbiesMembers_Lobbies_LobbyId",
                        column: x => x.LobbyId,
                        principalSchema: "matches",
                        principalTable: "Lobbies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LobbiesMembers_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalSchema: "matches",
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchesPlayers",
                schema: "matches",
                columns: table => new
                {
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SteamId = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: false),
                    Team = table.Column<int>(type: "int", nullable: false),
                    Stats_Kills = table.Column<int>(type: "int", nullable: false),
                    Stats_Assists = table.Column<int>(type: "int", nullable: false),
                    Stats_Deaths = table.Column<int>(type: "int", nullable: false),
                    Stats_Hs = table.Column<int>(type: "int", nullable: false),
                    RatingStart = table.Column<int>(type: "int", nullable: false),
                    RatingEnd = table.Column<int>(type: "int", nullable: false),
                    IsLeaver = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchesPlayers", x => new { x.MatchId, x.PlayerId });
                    table.ForeignKey(
                        name: "FK_MatchesPlayers_Matches_MatchId",
                        column: x => x.MatchId,
                        principalSchema: "matches",
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchesPlayers_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalSchema: "matches",
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LobbiesInvitations_InvitedPlayerId",
                schema: "matches",
                table: "LobbiesInvitations",
                column: "InvitedPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_LobbiesInvitations_InvitingPlayerId",
                schema: "matches",
                table: "LobbiesInvitations",
                column: "InvitingPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_LobbiesMembers_LobbyId",
                schema: "matches",
                table: "LobbiesMembers",
                column: "LobbyId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchesLobbies_LobbyId",
                schema: "matches",
                table: "MatchesLobbies",
                column: "LobbyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MatchesPlayers_PlayerId",
                schema: "matches",
                table: "MatchesPlayers",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LobbiesInvitations",
                schema: "matches");

            migrationBuilder.DropTable(
                name: "LobbiesMembers",
                schema: "matches");

            migrationBuilder.DropTable(
                name: "MatchesLobbies",
                schema: "matches");

            migrationBuilder.DropTable(
                name: "MatchesPlayers",
                schema: "matches");

            migrationBuilder.DropTable(
                name: "OutboxMessages",
                schema: "matches");

            migrationBuilder.DropTable(
                name: "Lobbies",
                schema: "matches");

            migrationBuilder.DropTable(
                name: "Matches",
                schema: "matches");

            migrationBuilder.DropTable(
                name: "Players",
                schema: "matches");
        }
    }
}
