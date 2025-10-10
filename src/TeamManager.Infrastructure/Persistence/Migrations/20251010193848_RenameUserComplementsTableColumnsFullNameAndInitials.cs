using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamManager.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenameUserComplementsTableColumnsFullNameAndInitials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Initials",
                schema: "TeamManager",
                table: "UserComplements",
                newName: "initials");

            migrationBuilder.RenameColumn(
                name: "FullName",
                schema: "TeamManager",
                table: "UserComplements",
                newName: "fullname");

            migrationBuilder.AlterColumn<string>(
                name: "initials",
                schema: "TeamManager",
                table: "UserComplements",
                type: "CHAR(2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "fullname",
                schema: "TeamManager",
                table: "UserComplements",
                type: "VARCHAR(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "initials",
                schema: "TeamManager",
                table: "UserComplements",
                newName: "Initials");

            migrationBuilder.RenameColumn(
                name: "fullname",
                schema: "TeamManager",
                table: "UserComplements",
                newName: "FullName");

            migrationBuilder.AlterColumn<string>(
                name: "Initials",
                schema: "TeamManager",
                table: "UserComplements",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "CHAR(2)");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                schema: "TeamManager",
                table: "UserComplements",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(100)");
        }
    }
}
