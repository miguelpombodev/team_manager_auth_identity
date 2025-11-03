using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamManager.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class AddDescriptionColumnInTeamsTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "team_description",
            schema: "TeamManager",
            table: "Teams",
            type: "VARCHAR(200)",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "team_description",
            schema: "TeamManager",
            table: "Teams");
    }
}