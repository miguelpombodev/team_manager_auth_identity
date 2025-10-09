using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamManager.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumnsToPascalCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_role_claims_roles_role_id",
                schema: "TeamManager",
                table: "RoleClaims");

            migrationBuilder.DropForeignKey(
                name: "fk_user_claims_asp_net_users_user_id",
                schema: "TeamManager",
                table: "UserClaims");

            migrationBuilder.DropForeignKey(
                name: "fk_user_logins_asp_net_users_user_id",
                schema: "TeamManager",
                table: "UserLogins");

            migrationBuilder.DropForeignKey(
                name: "fk_user_roles_asp_net_users_user_id",
                schema: "TeamManager",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "fk_user_roles_roles_role_id",
                schema: "TeamManager",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "fk_user_teams_application_user_user_id",
                schema: "TeamManager",
                table: "UserTeams");

            migrationBuilder.DropForeignKey(
                name: "fk_user_teams_teams_team_id",
                schema: "TeamManager",
                table: "UserTeams");

            migrationBuilder.DropForeignKey(
                name: "fk_user_tokens_asp_net_users_user_id",
                schema: "TeamManager",
                table: "UserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "pk_user_tokens",
                schema: "TeamManager",
                table: "UserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "pk_user_teams",
                schema: "TeamManager",
                table: "UserTeams");

            migrationBuilder.DropPrimaryKey(
                name: "pk_user_roles",
                schema: "TeamManager",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "pk_user_logins",
                schema: "TeamManager",
                table: "UserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "pk_user_complements",
                schema: "TeamManager",
                table: "UserComplements");

            migrationBuilder.DropPrimaryKey(
                name: "pk_user_claims",
                schema: "TeamManager",
                table: "UserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "pk_teams",
                schema: "TeamManager",
                table: "Teams");

            migrationBuilder.DropPrimaryKey(
                name: "pk_roles",
                schema: "TeamManager",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "pk_role_claims",
                schema: "TeamManager",
                table: "RoleClaims");

            migrationBuilder.DropPrimaryKey(
                name: "pk_application_user",
                schema: "TeamManager",
                table: "ApplicationUser");

            migrationBuilder.RenameColumn(
                name: "value",
                schema: "TeamManager",
                table: "UserTokens",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "TeamManager",
                table: "UserTokens",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "login_provider",
                schema: "TeamManager",
                table: "UserTokens",
                newName: "LoginProvider");

            migrationBuilder.RenameColumn(
                name: "user_id",
                schema: "TeamManager",
                table: "UserTokens",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "team_id",
                schema: "TeamManager",
                table: "UserTeams",
                newName: "TeamId");

            migrationBuilder.RenameColumn(
                name: "user_id",
                schema: "TeamManager",
                table: "UserTeams",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "ix_user_teams_team_id",
                schema: "TeamManager",
                table: "UserTeams",
                newName: "IX_UserTeams_TeamId");

            migrationBuilder.RenameColumn(
                name: "role_id",
                schema: "TeamManager",
                table: "UserRoles",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "user_id",
                schema: "TeamManager",
                table: "UserRoles",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "ix_user_roles_role_id",
                schema: "TeamManager",
                table: "UserRoles",
                newName: "IX_UserRoles_RoleId");

            migrationBuilder.RenameColumn(
                name: "user_id",
                schema: "TeamManager",
                table: "UserLogins",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "provider_display_name",
                schema: "TeamManager",
                table: "UserLogins",
                newName: "ProviderDisplayName");

            migrationBuilder.RenameColumn(
                name: "provider_key",
                schema: "TeamManager",
                table: "UserLogins",
                newName: "ProviderKey");

            migrationBuilder.RenameColumn(
                name: "login_provider",
                schema: "TeamManager",
                table: "UserLogins",
                newName: "LoginProvider");

            migrationBuilder.RenameIndex(
                name: "ix_user_logins_user_id",
                schema: "TeamManager",
                table: "UserLogins",
                newName: "IX_UserLogins_UserId");

            migrationBuilder.RenameColumn(
                name: "initials",
                schema: "TeamManager",
                table: "UserComplements",
                newName: "Initials");

            migrationBuilder.RenameColumn(
                name: "full_name",
                schema: "TeamManager",
                table: "UserComplements",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "TeamManager",
                table: "UserClaims",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                schema: "TeamManager",
                table: "UserClaims",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "claim_value",
                schema: "TeamManager",
                table: "UserClaims",
                newName: "ClaimValue");

            migrationBuilder.RenameColumn(
                name: "claim_type",
                schema: "TeamManager",
                table: "UserClaims",
                newName: "ClaimType");

            migrationBuilder.RenameIndex(
                name: "ix_user_claims_user_id",
                schema: "TeamManager",
                table: "UserClaims",
                newName: "IX_UserClaims_UserId");

            migrationBuilder.RenameIndex(
                name: "ix_teams_team_name",
                schema: "TeamManager",
                table: "Teams",
                newName: "IX_Teams_team_name");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "TeamManager",
                table: "Roles",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "TeamManager",
                table: "Roles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "normalized_name",
                schema: "TeamManager",
                table: "Roles",
                newName: "NormalizedName");

            migrationBuilder.RenameColumn(
                name: "concurrency_stamp",
                schema: "TeamManager",
                table: "Roles",
                newName: "ConcurrencyStamp");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "TeamManager",
                table: "RoleClaims",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "role_id",
                schema: "TeamManager",
                table: "RoleClaims",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "claim_value",
                schema: "TeamManager",
                table: "RoleClaims",
                newName: "ClaimValue");

            migrationBuilder.RenameColumn(
                name: "claim_type",
                schema: "TeamManager",
                table: "RoleClaims",
                newName: "ClaimType");

            migrationBuilder.RenameIndex(
                name: "ix_role_claims_role_id",
                schema: "TeamManager",
                table: "RoleClaims",
                newName: "IX_RoleClaims_RoleId");

            migrationBuilder.RenameColumn(
                name: "username",
                schema: "TeamManager",
                table: "ApplicationUser",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "email",
                schema: "TeamManager",
                table: "ApplicationUser",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "TeamManager",
                table: "ApplicationUser",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "security_stamp",
                schema: "TeamManager",
                table: "ApplicationUser",
                newName: "SecurityStamp");

            migrationBuilder.RenameColumn(
                name: "phone_number_confirmed",
                schema: "TeamManager",
                table: "ApplicationUser",
                newName: "PhoneNumberConfirmed");

            migrationBuilder.RenameColumn(
                name: "phone_number",
                schema: "TeamManager",
                table: "ApplicationUser",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "password_hash",
                schema: "TeamManager",
                table: "ApplicationUser",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "normalized_username",
                schema: "TeamManager",
                table: "ApplicationUser",
                newName: "NormalizedUserName");

            migrationBuilder.RenameColumn(
                name: "normalized_email",
                schema: "TeamManager",
                table: "ApplicationUser",
                newName: "NormalizedEmail");

            migrationBuilder.RenameColumn(
                name: "lockout_end",
                schema: "TeamManager",
                table: "ApplicationUser",
                newName: "LockoutEnd");

            migrationBuilder.RenameColumn(
                name: "lockout_enabled",
                schema: "TeamManager",
                table: "ApplicationUser",
                newName: "LockoutEnabled");

            migrationBuilder.RenameColumn(
                name: "f2a_enabled",
                schema: "TeamManager",
                table: "ApplicationUser",
                newName: "TwoFactorEnabled");

            migrationBuilder.RenameColumn(
                name: "email_confirmed",
                schema: "TeamManager",
                table: "ApplicationUser",
                newName: "EmailConfirmed");

            migrationBuilder.RenameColumn(
                name: "concurrency_stamp",
                schema: "TeamManager",
                table: "ApplicationUser",
                newName: "ConcurrencyStamp");

            migrationBuilder.RenameColumn(
                name: "access_failed_count",
                schema: "TeamManager",
                table: "ApplicationUser",
                newName: "AccessFailedCount");

            migrationBuilder.RenameIndex(
                name: "ix_application_user_id_email_username_normalized_username_norma",
                schema: "TeamManager",
                table: "ApplicationUser",
                newName: "IX_ApplicationUser_Id_Email_UserName_NormalizedUserName_Normal~");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTokens",
                schema: "TeamManager",
                table: "UserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTeams",
                schema: "TeamManager",
                table: "UserTeams",
                columns: new[] { "UserId", "TeamId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                schema: "TeamManager",
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLogins",
                schema: "TeamManager",
                table: "UserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserComplements",
                schema: "TeamManager",
                table: "UserComplements",
                columns: new[] { "id", "public_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserClaims",
                schema: "TeamManager",
                table: "UserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teams",
                schema: "TeamManager",
                table: "Teams",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                schema: "TeamManager",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleClaims",
                schema: "TeamManager",
                table: "RoleClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUser",
                schema: "TeamManager",
                table: "ApplicationUser",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleClaims_Roles_RoleId",
                schema: "TeamManager",
                table: "RoleClaims",
                column: "RoleId",
                principalSchema: "TeamManager",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaims_ApplicationUser_UserId",
                schema: "TeamManager",
                table: "UserClaims",
                column: "UserId",
                principalSchema: "TeamManager",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogins_ApplicationUser_UserId",
                schema: "TeamManager",
                table: "UserLogins",
                column: "UserId",
                principalSchema: "TeamManager",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_ApplicationUser_UserId",
                schema: "TeamManager",
                table: "UserRoles",
                column: "UserId",
                principalSchema: "TeamManager",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                schema: "TeamManager",
                table: "UserRoles",
                column: "RoleId",
                principalSchema: "TeamManager",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTeams_ApplicationUser_UserId",
                schema: "TeamManager",
                table: "UserTeams",
                column: "UserId",
                principalSchema: "TeamManager",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTeams_Teams_TeamId",
                schema: "TeamManager",
                table: "UserTeams",
                column: "TeamId",
                principalSchema: "TeamManager",
                principalTable: "Teams",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTokens_ApplicationUser_UserId",
                schema: "TeamManager",
                table: "UserTokens",
                column: "UserId",
                principalSchema: "TeamManager",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleClaims_Roles_RoleId",
                schema: "TeamManager",
                table: "RoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClaims_ApplicationUser_UserId",
                schema: "TeamManager",
                table: "UserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogins_ApplicationUser_UserId",
                schema: "TeamManager",
                table: "UserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_ApplicationUser_UserId",
                schema: "TeamManager",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                schema: "TeamManager",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTeams_ApplicationUser_UserId",
                schema: "TeamManager",
                table: "UserTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTeams_Teams_TeamId",
                schema: "TeamManager",
                table: "UserTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTokens_ApplicationUser_UserId",
                schema: "TeamManager",
                table: "UserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTokens",
                schema: "TeamManager",
                table: "UserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTeams",
                schema: "TeamManager",
                table: "UserTeams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                schema: "TeamManager",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLogins",
                schema: "TeamManager",
                table: "UserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserComplements",
                schema: "TeamManager",
                table: "UserComplements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserClaims",
                schema: "TeamManager",
                table: "UserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teams",
                schema: "TeamManager",
                table: "Teams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                schema: "TeamManager",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleClaims",
                schema: "TeamManager",
                table: "RoleClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUser",
                schema: "TeamManager",
                table: "ApplicationUser");

            migrationBuilder.RenameColumn(
                name: "Value",
                schema: "TeamManager",
                table: "UserTokens",
                newName: "value");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "TeamManager",
                table: "UserTokens",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "LoginProvider",
                schema: "TeamManager",
                table: "UserTokens",
                newName: "login_provider");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "TeamManager",
                table: "UserTokens",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                schema: "TeamManager",
                table: "UserTeams",
                newName: "team_id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "TeamManager",
                table: "UserTeams",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_UserTeams_TeamId",
                schema: "TeamManager",
                table: "UserTeams",
                newName: "ix_user_teams_team_id");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                schema: "TeamManager",
                table: "UserRoles",
                newName: "role_id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "TeamManager",
                table: "UserRoles",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_RoleId",
                schema: "TeamManager",
                table: "UserRoles",
                newName: "ix_user_roles_role_id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "TeamManager",
                table: "UserLogins",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "ProviderDisplayName",
                schema: "TeamManager",
                table: "UserLogins",
                newName: "provider_display_name");

            migrationBuilder.RenameColumn(
                name: "ProviderKey",
                schema: "TeamManager",
                table: "UserLogins",
                newName: "provider_key");

            migrationBuilder.RenameColumn(
                name: "LoginProvider",
                schema: "TeamManager",
                table: "UserLogins",
                newName: "login_provider");

            migrationBuilder.RenameIndex(
                name: "IX_UserLogins_UserId",
                schema: "TeamManager",
                table: "UserLogins",
                newName: "ix_user_logins_user_id");

            migrationBuilder.RenameColumn(
                name: "Initials",
                schema: "TeamManager",
                table: "UserComplements",
                newName: "initials");

            migrationBuilder.RenameColumn(
                name: "FullName",
                schema: "TeamManager",
                table: "UserComplements",
                newName: "full_name");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "TeamManager",
                table: "UserClaims",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "TeamManager",
                table: "UserClaims",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "ClaimValue",
                schema: "TeamManager",
                table: "UserClaims",
                newName: "claim_value");

            migrationBuilder.RenameColumn(
                name: "ClaimType",
                schema: "TeamManager",
                table: "UserClaims",
                newName: "claim_type");

            migrationBuilder.RenameIndex(
                name: "IX_UserClaims_UserId",
                schema: "TeamManager",
                table: "UserClaims",
                newName: "ix_user_claims_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_Teams_team_name",
                schema: "TeamManager",
                table: "Teams",
                newName: "ix_teams_team_name");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "TeamManager",
                table: "Roles",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "TeamManager",
                table: "Roles",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "NormalizedName",
                schema: "TeamManager",
                table: "Roles",
                newName: "normalized_name");

            migrationBuilder.RenameColumn(
                name: "ConcurrencyStamp",
                schema: "TeamManager",
                table: "Roles",
                newName: "concurrency_stamp");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "TeamManager",
                table: "RoleClaims",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                schema: "TeamManager",
                table: "RoleClaims",
                newName: "role_id");

            migrationBuilder.RenameColumn(
                name: "ClaimValue",
                schema: "TeamManager",
                table: "RoleClaims",
                newName: "claim_value");

            migrationBuilder.RenameColumn(
                name: "ClaimType",
                schema: "TeamManager",
                table: "RoleClaims",
                newName: "claim_type");

            migrationBuilder.RenameIndex(
                name: "IX_RoleClaims_RoleId",
                schema: "TeamManager",
                table: "RoleClaims",
                newName: "ix_role_claims_role_id");

            migrationBuilder.RenameColumn(
                name: "UserName",
                schema: "TeamManager",
                table: "ApplicationUser",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Email",
                schema: "TeamManager",
                table: "ApplicationUser",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "TeamManager",
                table: "ApplicationUser",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "TwoFactorEnabled",
                schema: "TeamManager",
                table: "ApplicationUser",
                newName: "f2a_enabled");

            migrationBuilder.RenameColumn(
                name: "SecurityStamp",
                schema: "TeamManager",
                table: "ApplicationUser",
                newName: "security_stamp");

            migrationBuilder.RenameColumn(
                name: "PhoneNumberConfirmed",
                schema: "TeamManager",
                table: "ApplicationUser",
                newName: "phone_number_confirmed");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                schema: "TeamManager",
                table: "ApplicationUser",
                newName: "phone_number");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                schema: "TeamManager",
                table: "ApplicationUser",
                newName: "password_hash");

            migrationBuilder.RenameColumn(
                name: "NormalizedUserName",
                schema: "TeamManager",
                table: "ApplicationUser",
                newName: "normalized_username");

            migrationBuilder.RenameColumn(
                name: "NormalizedEmail",
                schema: "TeamManager",
                table: "ApplicationUser",
                newName: "normalized_email");

            migrationBuilder.RenameColumn(
                name: "LockoutEnd",
                schema: "TeamManager",
                table: "ApplicationUser",
                newName: "lockout_end");

            migrationBuilder.RenameColumn(
                name: "LockoutEnabled",
                schema: "TeamManager",
                table: "ApplicationUser",
                newName: "lockout_enabled");

            migrationBuilder.RenameColumn(
                name: "EmailConfirmed",
                schema: "TeamManager",
                table: "ApplicationUser",
                newName: "email_confirmed");

            migrationBuilder.RenameColumn(
                name: "ConcurrencyStamp",
                schema: "TeamManager",
                table: "ApplicationUser",
                newName: "concurrency_stamp");

            migrationBuilder.RenameColumn(
                name: "AccessFailedCount",
                schema: "TeamManager",
                table: "ApplicationUser",
                newName: "access_failed_count");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUser_Id_Email_UserName_NormalizedUserName_Normal~",
                schema: "TeamManager",
                table: "ApplicationUser",
                newName: "ix_application_user_id_email_username_normalized_username_norma");

            migrationBuilder.AddPrimaryKey(
                name: "pk_user_tokens",
                schema: "TeamManager",
                table: "UserTokens",
                columns: new[] { "user_id", "login_provider", "name" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_user_teams",
                schema: "TeamManager",
                table: "UserTeams",
                columns: new[] { "user_id", "team_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_user_roles",
                schema: "TeamManager",
                table: "UserRoles",
                columns: new[] { "user_id", "role_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_user_logins",
                schema: "TeamManager",
                table: "UserLogins",
                columns: new[] { "login_provider", "provider_key" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_user_complements",
                schema: "TeamManager",
                table: "UserComplements",
                columns: new[] { "id", "public_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_user_claims",
                schema: "TeamManager",
                table: "UserClaims",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_teams",
                schema: "TeamManager",
                table: "Teams",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_roles",
                schema: "TeamManager",
                table: "Roles",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_role_claims",
                schema: "TeamManager",
                table: "RoleClaims",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_application_user",
                schema: "TeamManager",
                table: "ApplicationUser",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_role_claims_roles_role_id",
                schema: "TeamManager",
                table: "RoleClaims",
                column: "role_id",
                principalSchema: "TeamManager",
                principalTable: "Roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_user_claims_asp_net_users_user_id",
                schema: "TeamManager",
                table: "UserClaims",
                column: "user_id",
                principalSchema: "TeamManager",
                principalTable: "ApplicationUser",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_user_logins_asp_net_users_user_id",
                schema: "TeamManager",
                table: "UserLogins",
                column: "user_id",
                principalSchema: "TeamManager",
                principalTable: "ApplicationUser",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_user_roles_asp_net_users_user_id",
                schema: "TeamManager",
                table: "UserRoles",
                column: "user_id",
                principalSchema: "TeamManager",
                principalTable: "ApplicationUser",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_user_roles_roles_role_id",
                schema: "TeamManager",
                table: "UserRoles",
                column: "role_id",
                principalSchema: "TeamManager",
                principalTable: "Roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_user_teams_application_user_user_id",
                schema: "TeamManager",
                table: "UserTeams",
                column: "user_id",
                principalSchema: "TeamManager",
                principalTable: "ApplicationUser",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_user_teams_teams_team_id",
                schema: "TeamManager",
                table: "UserTeams",
                column: "team_id",
                principalSchema: "TeamManager",
                principalTable: "Teams",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_user_tokens_asp_net_users_user_id",
                schema: "TeamManager",
                table: "UserTokens",
                column: "user_id",
                principalSchema: "TeamManager",
                principalTable: "ApplicationUser",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
