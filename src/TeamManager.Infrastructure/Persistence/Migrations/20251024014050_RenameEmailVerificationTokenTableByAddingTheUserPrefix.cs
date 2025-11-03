using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamManager.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class RenameEmailVerificationTokenTableByAddingTheUserPrefix : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_EmailVerification_ApplicationUser_user_id",
            schema: "TeamManager",
            table: "EmailVerification");

        migrationBuilder.DropPrimaryKey(
            name: "PK_EmailVerification",
            schema: "TeamManager",
            table: "EmailVerification");

        migrationBuilder.RenameTable(
            name: "EmailVerification",
            schema: "TeamManager",
            newName: "UserEmailVerificationToken",
            newSchema: "TeamManager");

        migrationBuilder.RenameIndex(
            name: "IX_EmailVerification_user_id",
            schema: "TeamManager",
            table: "UserEmailVerificationToken",
            newName: "IX_UserEmailVerificationToken_user_id");

        migrationBuilder.AddPrimaryKey(
            name: "PK_UserEmailVerificationToken",
            schema: "TeamManager",
            table: "UserEmailVerificationToken",
            column: "id");

        migrationBuilder.AddForeignKey(
            name: "FK_UserEmailVerificationToken_ApplicationUser_user_id",
            schema: "TeamManager",
            table: "UserEmailVerificationToken",
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
            name: "FK_UserEmailVerificationToken_ApplicationUser_user_id",
            schema: "TeamManager",
            table: "UserEmailVerificationToken");

        migrationBuilder.DropPrimaryKey(
            name: "PK_UserEmailVerificationToken",
            schema: "TeamManager",
            table: "UserEmailVerificationToken");

        migrationBuilder.RenameTable(
            name: "UserEmailVerificationToken",
            schema: "TeamManager",
            newName: "EmailVerification",
            newSchema: "TeamManager");

        migrationBuilder.RenameIndex(
            name: "IX_UserEmailVerificationToken_user_id",
            schema: "TeamManager",
            table: "EmailVerification",
            newName: "IX_EmailVerification_user_id");

        migrationBuilder.AddPrimaryKey(
            name: "PK_EmailVerification",
            schema: "TeamManager",
            table: "EmailVerification",
            column: "id");

        migrationBuilder.AddForeignKey(
            name: "FK_EmailVerification_ApplicationUser_user_id",
            schema: "TeamManager",
            table: "EmailVerification",
            column: "user_id",
            principalSchema: "TeamManager",
            principalTable: "ApplicationUser",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}