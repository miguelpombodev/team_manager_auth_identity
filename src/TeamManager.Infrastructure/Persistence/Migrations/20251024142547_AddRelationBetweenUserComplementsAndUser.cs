using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamManager.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationBetweenUserComplementsAndUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserComplements",
                schema: "TeamManager",
                table: "UserComplements");

            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                schema: "TeamManager",
                table: "UserComplements",
                type: "UUID",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserComplements",
                schema: "TeamManager",
                table: "UserComplements",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_UserComplements_public_id",
                schema: "TeamManager",
                table: "UserComplements",
                column: "public_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserComplements_user_id",
                schema: "TeamManager",
                table: "UserComplements",
                column: "user_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserComplements_ApplicationUser_user_id",
                schema: "TeamManager",
                table: "UserComplements",
                column: "user_id",
                principalSchema: "TeamManager",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserComplements_ApplicationUser_user_id",
                schema: "TeamManager",
                table: "UserComplements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserComplements",
                schema: "TeamManager",
                table: "UserComplements");

            migrationBuilder.DropIndex(
                name: "IX_UserComplements_public_id",
                schema: "TeamManager",
                table: "UserComplements");

            migrationBuilder.DropIndex(
                name: "IX_UserComplements_user_id",
                schema: "TeamManager",
                table: "UserComplements");

            migrationBuilder.DropColumn(
                name: "user_id",
                schema: "TeamManager",
                table: "UserComplements");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserComplements",
                schema: "TeamManager",
                table: "UserComplements",
                columns: new[] { "id", "public_id" });
        }
    }
}
