using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamManager.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class AddRolesRelationshipInApplicationAuthUser : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<Guid>(
            name: "ApplicationAuthUserId",
            schema: "TeamManager",
            table: "Roles",
            type: "UUID",
            nullable: true);

        migrationBuilder.CreateIndex(
            name: "IX_Roles_ApplicationAuthUserId",
            schema: "TeamManager",
            table: "Roles",
            column: "ApplicationAuthUserId");

        migrationBuilder.AddForeignKey(
            name: "FK_Roles_ApplicationUser_ApplicationAuthUserId",
            schema: "TeamManager",
            table: "Roles",
            column: "ApplicationAuthUserId",
            principalSchema: "TeamManager",
            principalTable: "ApplicationUser",
            principalColumn: "Id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Roles_ApplicationUser_ApplicationAuthUserId",
            schema: "TeamManager",
            table: "Roles");

        migrationBuilder.DropIndex(
            name: "IX_Roles_ApplicationAuthUserId",
            schema: "TeamManager",
            table: "Roles");

        migrationBuilder.DropColumn(
            name: "ApplicationAuthUserId",
            schema: "TeamManager",
            table: "Roles");
    }
}