using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aimrank.Web.Database.Migrator.Migrations.Matches
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "matches");

            migrationBuilder.CreateTable(
                name: "inbox_messages",
                schema: "matches",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    occurred_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    processed_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    type = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    data = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_inbox_messages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "lobbies",
                schema: "matches",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    configuration_name = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    configuration_mode = table.Column<int>(type: "integer", nullable: false),
                    configuration_maps = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_lobbies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "matches",
                schema: "matches",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    map = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    mode = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    finished_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    address = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    score_ct = table.Column<int>(type: "integer", nullable: false),
                    score_t = table.Column<int>(type: "integer", nullable: false),
                    winner = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_matches", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "outbox_messages",
                schema: "matches",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    occurred_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    processed_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    type = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    data = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_outbox_messages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "players",
                schema: "matches",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    steam_id = table.Column<string>(type: "character varying(17)", maxLength: 17, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_players", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "matches_lobbies",
                schema: "matches",
                columns: table => new
                {
                    lobby_id = table.Column<Guid>(type: "uuid", nullable: false),
                    match_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_matches_lobbies", x => new { x.match_id, x.lobby_id });
                    table.ForeignKey(
                        name: "fk_matches_lobbies_lobbies_lobby_id",
                        column: x => x.lobby_id,
                        principalSchema: "matches",
                        principalTable: "lobbies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_matches_lobbies_matches_match_id",
                        column: x => x.match_id,
                        principalSchema: "matches",
                        principalTable: "matches",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "lobbies_invitations",
                schema: "matches",
                columns: table => new
                {
                    inviting_player_id = table.Column<Guid>(type: "uuid", nullable: false),
                    invited_player_id = table.Column<Guid>(type: "uuid", nullable: false),
                    lobby_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_lobbies_invitations", x => new { x.lobby_id, x.inviting_player_id, x.invited_player_id });
                    table.ForeignKey(
                        name: "fk_lobbies_invitations_lobbies_lobby_id",
                        column: x => x.lobby_id,
                        principalSchema: "matches",
                        principalTable: "lobbies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_lobbies_invitations_players_player_id",
                        column: x => x.invited_player_id,
                        principalSchema: "matches",
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_lobbies_invitations_players_player_id1",
                        column: x => x.inviting_player_id,
                        principalSchema: "matches",
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "lobbies_members",
                schema: "matches",
                columns: table => new
                {
                    player_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role = table.Column<int>(type: "integer", nullable: false),
                    lobby_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_lobbies_members", x => x.player_id);
                    table.ForeignKey(
                        name: "fk_lobbies_members_lobbies_lobby_id",
                        column: x => x.lobby_id,
                        principalSchema: "matches",
                        principalTable: "lobbies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_lobbies_members_players_player_id1",
                        column: x => x.player_id,
                        principalSchema: "matches",
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "matches_players",
                schema: "matches",
                columns: table => new
                {
                    player_id = table.Column<Guid>(type: "uuid", nullable: false),
                    match_id = table.Column<Guid>(type: "uuid", nullable: false),
                    steam_id = table.Column<string>(type: "character varying(17)", maxLength: 17, nullable: false),
                    team = table.Column<int>(type: "integer", nullable: false),
                    stats_kills = table.Column<int>(type: "integer", nullable: false),
                    stats_assists = table.Column<int>(type: "integer", nullable: false),
                    stats_deaths = table.Column<int>(type: "integer", nullable: false),
                    stats_hs = table.Column<int>(type: "integer", nullable: false),
                    rating_start = table.Column<int>(type: "integer", nullable: false),
                    rating_end = table.Column<int>(type: "integer", nullable: false),
                    is_leaver = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_matches_players", x => new { x.match_id, x.player_id });
                    table.ForeignKey(
                        name: "fk_matches_players_matches_match_id",
                        column: x => x.match_id,
                        principalSchema: "matches",
                        principalTable: "matches",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_matches_players_players_player_id",
                        column: x => x.player_id,
                        principalSchema: "matches",
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_lobbies_invitations_invited_player_id",
                schema: "matches",
                table: "lobbies_invitations",
                column: "invited_player_id");

            migrationBuilder.CreateIndex(
                name: "ix_lobbies_invitations_inviting_player_id",
                schema: "matches",
                table: "lobbies_invitations",
                column: "inviting_player_id");

            migrationBuilder.CreateIndex(
                name: "ix_lobbies_members_lobby_id",
                schema: "matches",
                table: "lobbies_members",
                column: "lobby_id");

            migrationBuilder.CreateIndex(
                name: "ix_matches_lobbies_lobby_id",
                schema: "matches",
                table: "matches_lobbies",
                column: "lobby_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_matches_players_player_id",
                schema: "matches",
                table: "matches_players",
                column: "player_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "inbox_messages",
                schema: "matches");

            migrationBuilder.DropTable(
                name: "lobbies_invitations",
                schema: "matches");

            migrationBuilder.DropTable(
                name: "lobbies_members",
                schema: "matches");

            migrationBuilder.DropTable(
                name: "matches_lobbies",
                schema: "matches");

            migrationBuilder.DropTable(
                name: "matches_players",
                schema: "matches");

            migrationBuilder.DropTable(
                name: "outbox_messages",
                schema: "matches");

            migrationBuilder.DropTable(
                name: "lobbies",
                schema: "matches");

            migrationBuilder.DropTable(
                name: "matches",
                schema: "matches");

            migrationBuilder.DropTable(
                name: "players",
                schema: "matches");
        }
    }
}
