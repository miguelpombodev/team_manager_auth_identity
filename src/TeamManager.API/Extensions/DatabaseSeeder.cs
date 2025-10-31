using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeamManager.Domain.Entities;
using TeamManager.Infrastructure.Persistence;

namespace TeamManager.API.Extensions;

public static class DatabaseSeeder
{
    public static async Task SeedDatabaseAsync(this IApplicationBuilder app)
    {
        await using (var scope = app.ApplicationServices.CreateAsyncScope())
        {
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("DatabaseSeeder");
            
            var configuration = services.GetRequiredService<IConfiguration>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var userManager = services.GetRequiredService<UserManager<ApplicationAuthUser>>();
            var context = services.GetRequiredService<ApplicationDbContext>();

            var strategy = context.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                var adminEmail = configuration["AdminUser:Email"];
                var adminFullName = configuration["AdminUser:FullName"];
                var adminPassword = configuration["AdminUser:Password"];

                if (string.IsNullOrEmpty(adminEmail) || string.IsNullOrEmpty(adminFullName) ||
                    string.IsNullOrEmpty(adminPassword))
                {
                    return;
                }

                if (!await roleManager.RoleExistsAsync(Roles.SystemAdmin))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(Roles.SystemAdmin));
                    logger.LogInformation("Role '{Role}' criada com sucesso.", Roles.SystemAdmin);
                }

                if (await userManager.FindByEmailAsync(adminEmail) is not null)
                {
                    logger.LogInformation("Utilizador SystemAdmin já existe. Nenhuma ação necessária.");
                    return;
                }
                
                await using (var transaction = await context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var adminUser = ApplicationAuthUser.Build(adminEmail, adminFullName);
                        var identityResult = await userManager.CreateAsync(adminUser, adminPassword);

                        if (!identityResult.Succeeded)
                        {
                            throw new InvalidOperationException(
                                $"Fail to create admin user: {string.Join(", ", identityResult.Errors.Select(e => e.Description))}");
                        }

                        var initials = new string(adminFullName.Split(' ').Where(s => s.Length > 0)
                            .Select(s => s[0]).Take(2).ToArray());
                        var complements = UserComplements.Build(adminFullName, adminUser.Id);

                        await context.UserComplements.AddAsync(complements);
                        await context.SaveChangesAsync();

                        var roleResult = await userManager.AddToRoleAsync(adminUser, Roles.SystemAdmin);
                        if (!roleResult.Succeeded)
                        {
                            throw new InvalidOperationException(
                                $"Fail to connect role to user: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
                        }

                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        logger.LogError(ex, "Falha ao executar o seeding do SystemAdmin. Rollback efetuado.");
                    }
                }
            });
        }
    }
}