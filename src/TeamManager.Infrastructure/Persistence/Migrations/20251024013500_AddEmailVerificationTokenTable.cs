using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamManager.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class AddEmailVerificationTokenTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "EmailVerification",
            schema: "TeamManager",
            columns: table => new
            {
                id = table.Column<Guid>(type: "UUID", nullable: false),
                user_id = table.Column<Guid>(type: "UUID", nullable: false),
                created_on_utc = table.Column<DateTime>(type: "TIMESTAMP WITH TIME ZONE", nullable: false),
                expires_on_utc = table.Column<DateTime>(type: "TIMESTAMP WITH TIME ZONE", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_EmailVerification", x => x.id);
                table.ForeignKey(
                    name: "FK_EmailVerification_ApplicationUser_user_id",
                    column: x => x.user_id,
                    principalSchema: "TeamManager",
                    principalTable: "ApplicationUser",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_EmailVerification_user_id",
            schema: "TeamManager",
            table: "EmailVerification",
            column: "user_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "EmailVerification",
            schema: "TeamManager");
    }
}