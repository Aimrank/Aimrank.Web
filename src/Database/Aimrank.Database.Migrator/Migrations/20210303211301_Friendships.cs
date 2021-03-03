using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aimrank.Database.Migrator.Migrations
{
    public partial class Friendships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Friendships",
                schema: "aimrank",
                columns: table => new
                {
                    User1Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    User2Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlockingUserId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BlockingUserId2 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InvitingUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendships", x => new { x.User1Id, x.User2Id });
                    table.ForeignKey(
                        name: "FK_Friendships_AspNetUsers_BlockingUserId1",
                        column: x => x.BlockingUserId1,
                        principalSchema: "aimrank",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Friendships_AspNetUsers_BlockingUserId2",
                        column: x => x.BlockingUserId2,
                        principalSchema: "aimrank",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Friendships_AspNetUsers_InvitingUserId",
                        column: x => x.InvitingUserId,
                        principalSchema: "aimrank",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Friendships_AspNetUsers_User1Id",
                        column: x => x.User1Id,
                        principalSchema: "aimrank",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Friendships_AspNetUsers_User2Id",
                        column: x => x.User2Id,
                        principalSchema: "aimrank",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_BlockingUserId1",
                schema: "aimrank",
                table: "Friendships",
                column: "BlockingUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_BlockingUserId2",
                schema: "aimrank",
                table: "Friendships",
                column: "BlockingUserId2");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_InvitingUserId",
                schema: "aimrank",
                table: "Friendships",
                column: "InvitingUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_User2Id",
                schema: "aimrank",
                table: "Friendships",
                column: "User2Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Friendships",
                schema: "aimrank");
        }
    }
}
