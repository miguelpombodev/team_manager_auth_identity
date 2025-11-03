using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TeamManager.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class StartingStructure : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "TeamManager");

        migrationBuilder.CreateTable(
            name: "ApplicationUser",
            schema: "TeamManager",
            columns: table => new
            {
                id = table.Column<Guid>(type: "UUID", nullable: false),
                username = table.Column<string>(type: "VARCHAR(100)", maxLength: 256, nullable: false),
                normalized_username = table.Column<string>(type: "VARCHAR(100)", maxLength: 256, nullable: false),
                email = table.Column<string>(type: "VARCHAR(100)", maxLength: 256, nullable: false),
                normalized_email = table.Column<string>(type: "VARCHAR(100)", maxLength: 256, nullable: false),
                email_confirmed = table.Column<bool>(type: "BOOL", nullable: false),
                password_hash = table.Column<string>(type: "TEXT", nullable: true),
                security_stamp = table.Column<string>(type: "TEXT", nullable: true),
                concurrency_stamp = table.Column<string>(type: "CHAR(36)", nullable: true),
                phone_number = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                phone_number_confirmed = table.Column<bool>(type: "BOOL", nullable: false),
                f2a_enabled = table.Column<bool>(type: "BOOL", nullable: false),
                lockout_end = table.Column<DateTimeOffset>(type: "TIMESTAMPTZ", nullable: true),
                lockout_enabled = table.Column<bool>(type: "BOOL", nullable: false),
                access_failed_count = table.Column<int>(type: "INT4", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_application_user", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "Roles",
            schema: "TeamManager",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                normalized_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                concurrency_stamp = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_roles", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "Teams",
            schema: "TeamManager",
            columns: table => new
            {
                id = table.Column<Guid>(type: "UUID", nullable: false),
                team_name = table.Column<string>(type: "VARCHAR(100)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_teams", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "UserComplements",
            schema: "TeamManager",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                public_id = table.Column<Guid>(type: "UUID", nullable: false),
                full_name = table.Column<string>(type: "text", nullable: false),
                initials = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_user_complements", x => new { x.id, x.public_id });
            });

        migrationBuilder.CreateTable(
            name: "UserClaims",
            schema: "TeamManager",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                user_id = table.Column<Guid>(type: "UUID", nullable: false),
                claim_type = table.Column<string>(type: "text", nullable: true),
                claim_value = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_user_claims", x => x.id);
                table.ForeignKey(
                    name: "fk_user_claims_asp_net_users_user_id",
                    column: x => x.user_id,
                    principalSchema: "TeamManager",
                    principalTable: "ApplicationUser",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "UserLogins",
            schema: "TeamManager",
            columns: table => new
            {
                login_provider = table.Column<string>(type: "text", nullable: false),
                provider_key = table.Column<string>(type: "text", nullable: false),
                provider_display_name = table.Column<string>(type: "text", nullable: true),
                user_id = table.Column<Guid>(type: "UUID", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_user_logins", x => new { x.login_provider, x.provider_key });
                table.ForeignKey(
                    name: "fk_user_logins_asp_net_users_user_id",
                    column: x => x.user_id,
                    principalSchema: "TeamManager",
                    principalTable: "ApplicationUser",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "UserTokens",
            schema: "TeamManager",
            columns: table => new
            {
                user_id = table.Column<Guid>(type: "UUID", nullable: false),
                login_provider = table.Column<string>(type: "text", nullable: false),
                name = table.Column<string>(type: "text", nullable: false),
                value = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_user_tokens", x => new { x.user_id, x.login_provider, x.name });
                table.ForeignKey(
                    name: "fk_user_tokens_asp_net_users_user_id",
                    column: x => x.user_id,
                    principalSchema: "TeamManager",
                    principalTable: "ApplicationUser",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "RoleClaims",
            schema: "TeamManager",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                role_id = table.Column<Guid>(type: "uuid", nullable: false),
                claim_type = table.Column<string>(type: "text", nullable: true),
                claim_value = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_role_claims", x => x.id);
                table.ForeignKey(
                    name: "fk_role_claims_roles_role_id",
                    column: x => x.role_id,
                    principalSchema: "TeamManager",
                    principalTable: "Roles",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "UserRoles",
            schema: "TeamManager",
            columns: table => new
            {
                user_id = table.Column<Guid>(type: "UUID", nullable: false),
                role_id = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_user_roles", x => new { x.user_id, x.role_id });
                table.ForeignKey(
                    name: "fk_user_roles_asp_net_users_user_id",
                    column: x => x.user_id,
                    principalSchema: "TeamManager",
                    principalTable: "ApplicationUser",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_user_roles_roles_role_id",
                    column: x => x.role_id,
                    principalSchema: "TeamManager",
                    principalTable: "Roles",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "UserTeams",
            schema: "TeamManager",
            columns: table => new
            {
                user_id = table.Column<Guid>(type: "UUID", nullable: false),
                team_id = table.Column<Guid>(type: "UUID", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_user_teams", x => new { x.user_id, x.team_id });
                table.ForeignKey(
                    name: "fk_user_teams_application_user_user_id",
                    column: x => x.user_id,
                    principalSchema: "TeamManager",
                    principalTable: "ApplicationUser",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_user_teams_teams_team_id",
                    column: x => x.team_id,
                    principalSchema: "TeamManager",
                    principalTable: "Teams",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "EmailIndex",
            schema: "TeamManager",
            table: "ApplicationUser",
            column: "normalized_email");

        migrationBuilder.CreateIndex(
            name: "ix_application_user_id_email_username_normalized_username_norma",
            schema: "TeamManager",
            table: "ApplicationUser",
            columns: new[] { "id", "email", "username", "normalized_username", "normalized_email" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "UserNameIndex",
            schema: "TeamManager",
            table: "ApplicationUser",
            column: "normalized_username",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_role_claims_role_id",
            schema: "TeamManager",
            table: "RoleClaims",
            column: "role_id");

        migrationBuilder.CreateIndex(
            name: "RoleNameIndex",
            schema: "TeamManager",
            table: "Roles",
            column: "normalized_name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_teams_team_name",
            schema: "TeamManager",
            table: "Teams",
            column: "team_name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_user_claims_user_id",
            schema: "TeamManager",
            table: "UserClaims",
            column: "user_id");

        migrationBuilder.CreateIndex(
            name: "ix_user_logins_user_id",
            schema: "TeamManager",
            table: "UserLogins",
            column: "user_id");

        migrationBuilder.CreateIndex(
            name: "ix_user_roles_role_id",
            schema: "TeamManager",
            table: "UserRoles",
            column: "role_id");

        migrationBuilder.CreateIndex(
            name: "ix_user_teams_team_id",
            schema: "TeamManager",
            table: "UserTeams",
            column: "team_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "RoleClaims",
            schema: "TeamManager");

        migrationBuilder.DropTable(
            name: "UserClaims",
            schema: "TeamManager");

        migrationBuilder.DropTable(
            name: "UserComplements",
            schema: "TeamManager");

        migrationBuilder.DropTable(
            name: "UserLogins",
            schema: "TeamManager");

        migrationBuilder.DropTable(
            name: "UserRoles",
            schema: "TeamManager");

        migrationBuilder.DropTable(
            name: "UserTeams",
            schema: "TeamManager");

        migrationBuilder.DropTable(
            name: "UserTokens",
            schema: "TeamManager");

        migrationBuilder.DropTable(
            name: "Roles",
            schema: "TeamManager");

        migrationBuilder.DropTable(
            name: "Teams",
            schema: "TeamManager");

        migrationBuilder.DropTable(
            name: "ApplicationUser",
            schema: "TeamManager");
    }
}