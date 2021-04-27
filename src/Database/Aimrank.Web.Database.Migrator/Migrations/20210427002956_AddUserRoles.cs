using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aimrank.Web.Database.Migrator.Migrations
{
    public partial class AddUserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users_roles",
                schema: "users",
                columns: table => new
                {
                    name = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users_roles", x => new { x.user_id, x.name });
                    table.ForeignKey(
                        name: "fk_users_roles_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "users",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users_roles",
                schema: "users");
        }
    }
}
